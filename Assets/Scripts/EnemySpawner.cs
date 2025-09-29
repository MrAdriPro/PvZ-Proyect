using NaughtyAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner Settings")]
    [BoxGroup("Spawner Settings")]
    public Transform[] spawnerPoints;
    [BoxGroup("Spawner Settings")]
    public GameObject enemyPrefab;
    [BoxGroup("Spawner Settings")]
    public float spawnInterval = 2f;
    [BoxGroup("Spawner Settings")]
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    /// <summary>
    /// This just spawns an enemy at a random spawner point
    /// </summary>
    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnerPoints.Length);
        Transform spawnPoint = spawnerPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
