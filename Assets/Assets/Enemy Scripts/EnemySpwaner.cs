using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array to hold different enemy prefabs
    public float minRadius = 8f;
    public float maxRadius = 10f;

    public float timeBetweenWaves = 5f;
    public int baseEnemiesPerWave = 3;
    public int maxEnemies = 50;  // Maximum total number of enemies to spawn in the scene

    public LevelUI levelUI; // Drag in inspector

    private float timer = 0f;
    private int waveNumber = 1;
    private bool firstWaveDone = false;

    private int currentEnemyCount = 0;  // Track how many enemies are in the scene

    void Start()
    {
        StartCoroutine(BeginFirstLevel());
    }

    IEnumerator BeginFirstLevel()
    {
        levelUI.ShowLevel(1); // Show "Level 1" message
        yield return new WaitForSeconds(3f); // Wait before spawning
        SpawnWave(); // Spawn wave 1
        waveNumber++; // Next wave will be wave 2
        firstWaveDone = true;
    }

    void Update()
    {
        if (!firstWaveDone || currentEnemyCount >= maxEnemies) return;

        timer += Time.deltaTime;

        if (timer >= timeBetweenWaves)
        {
            SpawnWave();
            waveNumber++;
            timer = 0f;
        }
    }

    void SpawnWave()
    {
        int enemiesToSpawn = waveNumber * baseEnemiesPerWave;

        // Ensure we don't exceed the max enemy limit
        if (currentEnemyCount + enemiesToSpawn > maxEnemies)
        {
            enemiesToSpawn = maxEnemies - currentEnemyCount;  // Adjust to not exceed maxEnemies
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Select a random enemy type from the array
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Calculate spawn position
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * distance;

            // Instantiate the selected enemy
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Increment the enemy count
            currentEnemyCount++;
        }

        Debug.Log($"Wave {waveNumber} spawned: {enemiesToSpawn} enemies");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }
}
