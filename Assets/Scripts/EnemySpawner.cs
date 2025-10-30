using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject prefab;
        public int cost;
    }

    [Header("Enemy Spawner Settings")]
    [BoxGroup("Spawner Config")]
    public Transform[] spawnerPoints;

    [BoxGroup("Spawner Config")]
    public List<EnemyType> availableEnemies;

    [BoxGroup("Tasa de Spawn")]
    public float startSpawnInterval = 10f;
    [BoxGroup("Tasa de Spawn")]
    public float minSpawnInterval = 3f;
    [BoxGroup("Tasa de Spawn")]
    public float timeToMaxRate = 300f;

    [BoxGroup("Tasa de Spawn")]
    public float spawnBurstDelay = 0.5f;

    [BoxGroup("Cantidad/Presupuesto")]
    public int startSpawnAmount = 1;
    [BoxGroup("Cantidad/Presupuesto")]
    public int maxSpawnAmount = 5;
    [BoxGroup("Cantidad/Presupuesto")]
    public float timeToMaxAmount = 300f;

    [BoxGroup("Límite por Línea")]
    public int maxEnemiesPerLine = 8;

    [BoxGroup("Límite por Línea")]
    public float spawnPointOccupationRadius = 2f;

    [BoxGroup("Contenedor")]
    public Transform enemyParent;

    [BoxGroup("Runtime Data")]
    private float timer;
    [BoxGroup("Runtime Data")]
    [SerializeField] private float elapsedTime;

    private int[] currentLineCounts;
    private bool isSpawning = false;

    private void Start()
    {
        if (spawnerPoints != null)
        {
            currentLineCounts = new int[spawnerPoints.Length];
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float tRate = timeToMaxRate > 0f ? Mathf.Clamp01(elapsedTime / timeToMaxRate) : 1f;
        float currentInterval = Mathf.Lerp(startSpawnInterval, minSpawnInterval, tRate);

        float tCount = timeToMaxAmount > 0f ? Mathf.Clamp01(elapsedTime / timeToMaxAmount) : 1f;
        int currentSpawnAmount = Mathf.Max(1, Mathf.RoundToInt(Mathf.Lerp(startSpawnAmount, maxSpawnAmount, tCount)));

        timer += Time.deltaTime;

        if (timer >= currentInterval && !isSpawning)
        {
            StartCoroutine(SpawnEnemiesUsingBudget(currentSpawnAmount));
            timer = 0f;
        }
    }

    private IEnumerator SpawnEnemiesUsingBudget(int budget)
    {
        isSpawning = true;
        if (budget <= 0 || availableEnemies.Count == 0)
        {
            isSpawning = false;
            yield break;
        }

        int remainingBudget = budget;

        while (remainingBudget > 0)
        {
            List<int> availableLineIndices = new List<int>();
            for (int i = 0; i < spawnerPoints.Length; i++)
            {
                if (currentLineCounts[i] < maxEnemiesPerLine)
                {
                    availableLineIndices.Add(i);
                }
            }

            if (availableLineIndices.Count == 0)
            {
                break;
            }

            EnemyType enemyToSpawn = SelectAffordableEnemy(remainingBudget);

            if (enemyToSpawn.prefab == null)
            {
                break;
            }

            int chosenIndexInList = Random.Range(0, availableLineIndices.Count);
            int lineIndex = availableLineIndices[chosenIndexInList];
            Transform spawnPoint = spawnerPoints[lineIndex];

            if (IsSpawnPointOccupied(spawnPoint))
            {
                availableLineIndices.RemoveAt(chosenIndexInList);
                continue;
            }

            GameObject enemyInstance = Instantiate(enemyToSpawn.prefab, spawnPoint.position, Quaternion.identity);
            if (enemyParent != null)
                enemyInstance.transform.SetParent(enemyParent);

            remainingBudget -= enemyToSpawn.cost;
            currentLineCounts[lineIndex]++;

            Zombie comp = enemyInstance.GetComponent<Zombie>();
            if (comp != null)
            {
                comp.SetSpawnerReference(this, lineIndex, enemyToSpawn.cost);
            }

            if (spawnBurstDelay > 0)
            {
                yield return new WaitForSeconds(spawnBurstDelay);
            }
        }

        isSpawning = false;
    }

    private EnemyType SelectAffordableEnemy(int budget)
    {
        List<EnemyType> affordableEnemies = new List<EnemyType>();
        foreach (var enemy in availableEnemies)
        {
            if (enemy.cost <= budget)
            {
                affordableEnemies.Add(enemy);
            }
        }

        if (affordableEnemies.Count == 0)
        {
            return new EnemyType { prefab = null, cost = 0 };
        }

        return affordableEnemies[Random.Range(0, affordableEnemies.Count)];
    }

    private bool IsSpawnPointOccupied(Transform spawnPoint)
    {
        if (enemyParent == null) return false;

        float sqrRadius = spawnPointOccupationRadius * spawnPointOccupationRadius;

        for (int i = 0; i < enemyParent.childCount; i++)
        {
            Transform child = enemyParent.GetChild(i);
            if ((child.position - spawnPoint.position).sqrMagnitude <= sqrRadius)
                return true;
        }
        return false;
    }

    public void EnemyDied(int lineIndex)
    {
        if (lineIndex >= 0 && lineIndex < currentLineCounts.Length)
        {
            currentLineCounts[lineIndex] = Mathf.Max(0, currentLineCounts[lineIndex] - 1);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnerPoints != null)
        {
            Gizmos.color = Color.cyan;
            foreach (var point in spawnerPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, spawnPointOccupationRadius);
                }
            }
        }
    }
}