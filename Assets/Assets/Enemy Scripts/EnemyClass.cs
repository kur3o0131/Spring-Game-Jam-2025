using UnityEngine;

public abstract class EnemyClass : MonoBehaviour
{
    // a base class for the enemies : only have 2
    public float moveSpeed = 2f;
    protected float health;

    protected Transform target;
    protected Rigidbody2D rb;

    // adjusting the sound effect for when the enemy dies
    [Header("Death Sound")]
    public AudioClip dyingSFX;
    [Range(0f, 1f)] public float enemykillsound = 0.7f;
    private AudioSource sfxsource;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sfxsource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // when enemy die play the kill sound
        if (sfxsource && dyingSFX)
        {
            sfxsource.PlayOneShot(dyingSFX, enemykillsound);
        }
        // tell the enemy spawner that an enemy has died for tracker
        FindFirstObjectByType<spawnbugs>()?.OnEnemyDeath();
        // destroy after sound played
        Destroy(gameObject, 0.1f);
    }
}
