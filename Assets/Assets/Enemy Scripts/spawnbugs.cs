using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class spawnbugs : MonoBehaviour
{
    // instantiate text used for tracking number of bugs
    public TMP_Text totalbugsleft;

    // makiing an array of different bugs
    public GameObject[] differentbugss;

    // setting the min and max radius to set where bugs can spawn
    public float minRadius = 8f;
    public float maxRadius = 10f;

    // setting up waves
    public float waveIntervals = 5f;
    public int baseEnemyNumber = 3;
    public int maxActivebugs = 50;
    public int totalpossiblebugs = 200;

    // text to show what level it curerntly is
    public LevelUI leveltext;
    public int current_level = 1;
    public static int current_level_static = 1;

    private float timer = 0f;
    private int waveNumber = 1;
    private bool firstWaveDone = false;

    private int currentEnemyCount = 0;
    private int totalEnemiesSpawned = 0;
    private int enemiesKilled = 0;
    private bool levelComplete = false;

    void Start()
    {
        updatekillcounter();
        StartCoroutine(levelbegin());
    }

    // begin level
    IEnumerator levelbegin()
    {
        leveltext.displaylevel(1);
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnWave(baseEnemyNumber));
        waveNumber++;
        firstWaveDone = true;
    }

    void Update()
    {
        if ((totalpossiblebugs - enemiesKilled) <= 0 && !levelComplete)
        {
 
            levelComplete = true;
            StartCoroutine(transition_to_next_scene());
        }
        // checkin if the wave is done or if there are too many bugs
        if (!firstWaveDone || currentEnemyCount >= maxActivebugs || totalEnemiesSpawned >= totalpossiblebugs)
            return;
        timer += Time.deltaTime;
        if (timer >= waveIntervals)
        {
            int enemiesToSpawn = waveNumber * baseEnemyNumber;
            int remainingToSpawn = totalpossiblebugs - totalEnemiesSpawned;
            int remainingSlots = maxActivebugs - currentEnemyCount;
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
            // picks a random bug from the array
            GameObject differentbugs = differentbugss[Random.Range(0, differentbugss.Length)];
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * distance;
            Instantiate(differentbugs, spawnPos, Quaternion.identity);
            currentEnemyCount++;
            totalEnemiesSpawned++;
            updatekillcounter();
            // to not make all bugs spawn at teh same time
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
        enemiesKilled++;
        updatekillcounter();
    }

    // to update the total kill count
    void updatekillcounter()
    {
        int remaining = totalpossiblebugs - enemiesKilled;
        totalbugsleft.text = $"Enemies Remaining: {remaining}";
    }

    // just to visually see where bugs can spawn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    IEnumerator transition_to_next_scene()
    {
        yield return new WaitForSeconds(2f);
        int next_level = current_level + 1;
        current_level_static = next_level;
        string next_scene = "level" + next_level;
        SceneManager.LoadScene(next_scene);
    }
}
