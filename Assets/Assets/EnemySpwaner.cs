using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minRadius = 8f;
    public float maxRadius = 10f;

    public float timeBetweenWaves = 5f;
    public int baseEnemiesPerWave = 3;

    private float timer = 0f;
    private int waveNumber = 1;

    void Start()
    {
        SpawnWave(); // spawn wave 1 immediately
        waveNumber++; // now we're on wave 2
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    void Update()
    {
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

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * distance;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log($"Wave {waveNumber} spawned: {enemiesToSpawn} enemies");
    }
}
