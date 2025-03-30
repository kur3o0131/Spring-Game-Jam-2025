using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 2f;
    protected float health;
    protected Transform target;
    protected Rigidbody2D rb;

    [Header("Death Sound")]
    public AudioClip deathSFX;
    [Range(0f, 1f)] public float deathVolume = 0.7f;
    private AudioSource sfxSource;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sfxSource = GetComponent<AudioSource>();
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
        // play sound before destruction
        if (sfxSource && deathSFX)
        {
            sfxSource.PlayOneShot(deathSFX, deathVolume);
        }

        // notify spawner
        FindFirstObjectByType<EnemySpawner>()?.OnEnemyDeath();

        // destroy shortly after to allow sound to play
        Destroy(gameObject, 0.1f);
    }
}
