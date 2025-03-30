using UnityEngine;
using System.Collections;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public TextMeshProUGUI enemyCounterText;

    public GameObject[] enemyPrefabs;
    public float minRadius = 8f;
    public float maxRadius = 10f;

    public float timeBetweenWaves = 5f;
    public int baseEnemiesPerWave = 3;
    public int maxActiveEnemies = 50;      // How many enemies can be alive at once
    public int maxTotalSpawns = 200;       // Total enemies this spawner will ever produce

    public LevelUI levelUI;

    private float timer = 0f;
    private int waveNumber = 1;
    private bool firstWaveDone = false;

    private int currentEnemyCount = 0;     // Enemies currently in scene
    private int totalEnemiesSpawned = 0;   // All enemies spawned since start
    private int enemiesKilled = 0;

    void Start()
    {
        UpdateEnemyCounterUI();
        StartCoroutine(BeginFirstLevel());
    }

    IEnumerator BeginFirstLevel()
    {
        levelUI.ShowLevel(1);
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnWave(baseEnemiesPerWave));
        waveNumber++;
        firstWaveDone = true;
    }

    void Update()
    {
        if (!firstWaveDone || currentEnemyCount >= maxActiveEnemies || totalEnemiesSpawned >= maxTotalSpawns)
            return;

        timer += Time.deltaTime;

        if (timer >= timeBetweenWaves)
        {
            int enemiesToSpawn = waveNumber * baseEnemiesPerWave;

            // Clamp to not exceed limits
            int remainingToSpawn = maxTotalSpawns - totalEnemiesSpawned;
            int remainingSlots = maxActiveEnemies - currentEnemyCount;
            enemiesToSpawn = Mathf.Min(enemiesToSpawn, remainingToSpawn, remainingSlots);

            StartCoroutine(SpawnWave(enemiesToSpawn));
            waveNumber++;
            timer = 0f;
        }
    }

    IEnumerator SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * distance;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemyCount++;
            totalEnemiesSpawned++;
            UpdateEnemyCounterUI();

            yield return new WaitForSeconds(0.5f); // Delay between individual spawns
        }

        Debug.Log($"Wave {waveNumber} spawned: {count} enemies (Total spawned: {totalEnemiesSpawned})");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    // Called by enemy when it dies
    public void OnEnemyDeath()
    {
        currentEnemyCount--;
        enemiesKilled++;
        UpdateEnemyCounterUI();
    }

    void UpdateEnemyCounterUI()
    {
        int remaining = maxTotalSpawns - enemiesKilled;
        enemyCounterText.text = $"Enemies Remaining: {remaining}";
    }
}
