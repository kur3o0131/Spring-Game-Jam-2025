using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 2f;
    protected float health;
    protected Transform target;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        FindFirstObjectByType<EnemySpawner>()?.OnEnemyDeath();
        Destroy(gameObject);
    }
}
