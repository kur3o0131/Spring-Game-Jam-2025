using UnityEngine;

public class RangedEnemy : EnemyClass
{
    // create gmae object to store the projectile 
    public GameObject Bugprojectile;

    // instantiate the settings for the range
    public float attackRange = 5f;
    public float fireRate = 1.5f;
    private float fireCooldown = 0f;

    // animator for ranged movement
    public Animator anim;

    protected override void Start()
    {
        // ranged starts with 2 hearts
        base.Start();
        health = 2f;
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        // no player = player is dead
        if (!target) return;
        // find out the distance between the player and enemy
        float distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;

        // Update animation parameters for movement
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
        anim.SetBool("Moving", distance > 0.1f);

        // If the player is within the range of player
        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            fireCooldown -= Time.deltaTime;
            // shoot at player when cooldown is off
            if (fireCooldown <= 0f)
            {
                Shoot(direction);
                fireCooldown = fireRate;
            }
        }
        else
        {
            // moving towards player
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    // shoot method for the ranged enemy
    void Shoot(Vector2 direction)
    {
        GameObject proj = Instantiate(Bugprojectile, transform.position, Quaternion.identity);
        Projectile p = proj.GetComponent<Projectile>();
        if (p)
            p.SetDirection(direction);
    }
}
