using UnityEngine;

public class RangedEnemy : EnemyBase
{
    public GameObject projectilePrefab;
    public float attackRange = 5f;
    public float fireRate = 1.5f;
    private float fireCooldown = 0f;

    public Animator anim;

    protected override void Start()
    {
        base.Start();
        health = 2f;
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!target) return;

        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 dir = (target.position - transform.position).normalized;

        // Update animation parameters for movement
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
        anim.SetBool("Moving", distance > 0.1f);

        // If within range, stop and shoot
        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                Shoot(dir);
                fireCooldown = fireRate;
            }
        }
        else
        {
            // Move towards player
            rb.linearVelocity = dir * moveSpeed;
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile p = proj.GetComponent<Projectile>();
        if (p)
            p.SetDirection(direction);
    }
}
