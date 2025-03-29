using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    float health, maxHealth = 3f;

    public Animator anim;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player").transform;
        health = maxHealth;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            // set animation parameters
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
            anim.SetBool("Moving", direction.magnitude > 0.1f);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
