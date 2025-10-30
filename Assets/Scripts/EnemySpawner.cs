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
    [Tooltip("Intervalo inicial entre spawn (segundos) — más alto = menos enemigos al principio)")]
    public float startSpawnInterval = 3f;
    [BoxGroup("Spawner Settings")]
    [Tooltip("Intervalo mínimo entre spawn (segundos) — límite de rapidez)")]
    public float minSpawnInterval = 0.5f;
    [BoxGroup("Spawner Settings")]
    [Tooltip("Tiempo (segundos) que tarda en alcanzar la tasa máxima (minSpawnInterval)")]
    public float timeToMaxRate = 300f;
    [BoxGroup("Spawner Settings")]
    [Tooltip("Límite público de enemigos activos permitidos.")]
    public float maxActiveEnemies = 30f;
    [BoxGroup("Spawner Settings")]
    [Tooltip("Cantidad inicial de enemigos que se generan por evento de spawn")]
    public float startEnemiesPerSpawn = 1f;
    [BoxGroup("Spawner Settings")]
    [Tooltip("Cantidad máxima de enemigos por evento de spawn")]
    public float maxEnemiesPerSpawn = 5f;

    [BoxGroup("Spawner Settings")]
    [Tooltip("Tiempo (segundos) que tarda en alcanzar la cantidad máxima por spawn")]
    public float timeToMaxEnemies = 300f;

    [BoxGroup("Spawner Settings")]
    [Tooltip("Radio usado para determinar si un spawn point está ocupado (mismos valores en unidades del mundo).")]
    public float spawnPointOccupationRadius = 0.5f;

    [BoxGroup("Spawner Settings")]
    private float timer;
    [BoxGroup("Spawner Settings")]
    [SerializeField] private float elapsedTime;

    [BoxGroup("Spawner Settings")]
    public Transform enemyParent; 

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float tRate = timeToMaxRate > 0f ? Mathf.Clamp01(elapsedTime / timeToMaxRate) : 1f;
        float currentInterval = Mathf.Lerp(startSpawnInterval, minSpawnInterval, tRate);

        float tCount = timeToMaxEnemies > 0f ? Mathf.Clamp01(elapsedTime / timeToMaxEnemies) : 1f;
        int enemiesPerSpawn = Mathf.Max(1, Mathf.RoundToInt(Mathf.Lerp(startEnemiesPerSpawn, maxEnemiesPerSpawn, tCount)));

        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            int maxActive = Mathf.Max(0, Mathf.FloorToInt(maxActiveEnemies));
            int active = enemyParent ? enemyParent.childCount : 0;

            if (active < maxActive)
            {
                int canSpawn = Mathf.Min(enemiesPerSpawn, maxActive - active);
                SpawnMultipleEnemies(canSpawn);
            }

            timer = 0f;
        }
    }

    private void SpawnMultipleEnemies(int count)
    {
        if (count <= 0) return;
        if (spawnerPoints == null || spawnerPoints.Length == 0 || enemyPrefab == null)
            return;

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnerPoints.Length; i++) availableIndices.Add(i);

        HashSet<int> usedThisBatch = new HashSet<int>();

        for (int i = 0; i < count; i++)
        {
            List<int> candidates = new List<int>();
            foreach (int idx in availableIndices)
            {
                if (usedThisBatch.Contains(idx)) continue;
                Transform sp = spawnerPoints[idx];
                if (!IsSpawnPointOccupied(sp))
                    candidates.Add(idx);
            }

            if (candidates.Count == 0)
            {
                break;
            }

            int chosen = candidates[Random.Range(0, candidates.Count)];
            usedThisBatch.Add(chosen);

            Transform spawnPoint = spawnerPoints[chosen];
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            if (enemyParent != null)
                enemyInstance.transform.SetParent(enemyParent);
        }
    }

    private bool IsSpawnPointOccupied(Transform spawnPoint)
    {
        if (enemyParent == null) return false;

        float sqrRadius = spawnPointOccupationRadius * spawnPointOccupationRadius;
        for (int i = 0; i < enemyParent.childCount; i++)
        {
            Transform child = enemyParent.GetChild(i);
            if (child == null) continue;
            if ((child.position - spawnPoint.position).sqrMagnitude <= sqrRadius)
                return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(spawnerPoints[0].position, spawnPointOccupationRadius);
    }
}
