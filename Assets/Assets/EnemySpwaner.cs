using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberToSpawn = 5;
    public float minRadius = 8f;
    public float maxRadius = 10f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * distance;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
