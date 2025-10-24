using NaughtyAttributes;
using System.Collections;
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
    [Tooltip("Intervalo inicial entre spawn (segundos) — más alto = menos enemigos al principio")]
    public float startSpawnInterval = 3f;

    [BoxGroup("Spawner Settings")]
    [Tooltip("Intervalo mínimo entre spawn (segundos) — límite de rapidez")]
    public float minSpawnInterval = 0.5f;

    [BoxGroup("Spawner Settings")]
    [Tooltip("Tiempo (segundos) que tarda en alcanzar la tasa máxima (minSpawnInterval)")]
    public float timeToMaxRate = 300f;

    [BoxGroup("Spawner Settings")]
    [Tooltip("Límite público de enemigos activos permitidos.")]
    public float maxActiveEnemies = 30f;

    [BoxGroup("Spawner Settings")]
    private float timer;
    [BoxGroup("Spawner Settings")]
    [SerializeField] private float elapsedTime;

    [BoxGroup("Spawner Settings")]
    public Transform enemyParent; 

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = timeToMaxRate > 0f ? Mathf.Clamp01(elapsedTime / timeToMaxRate) : 1f;

        float currentInterval = Mathf.Lerp(startSpawnInterval, minSpawnInterval, t);

        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            int maxActive = Mathf.Max(0, Mathf.FloorToInt(maxActiveEnemies));
            int active = enemyParent ? enemyParent.childCount : 0;

            if (active < maxActive)
            {
                SpawnEnemy();
            }

            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnerPoints == null || spawnerPoints.Length == 0 || enemyPrefab == null)
            return;

        int index = Random.Range(0, spawnerPoints.Length);
        Transform spawnPoint = spawnerPoints[index];
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        if (enemyParent != null)
            enemyInstance.transform.SetParent(enemyParent);
    }
}
