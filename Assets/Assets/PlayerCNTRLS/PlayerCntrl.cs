using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCntrl : MonoBehaviour
{
    public HealthUI healthUI;
    public int maxHearts = 3;
    private int currentHearts;

    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    private GameManager gm;

    public Transform aim;
    bool isWalking = false;

    public Animator anim;

    public float moveSpeed;

    private Rigidbody2D rb;

    private float x;
    private float y;

    private Vector2 input;
    private bool moving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHearts = maxHearts;
        healthUI.SetHearts(currentHearts);
        rb = GetComponent<Rigidbody2D>();
        gm = Object.FindFirstObjectByType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;

        if(isWalking)
        {
            Vector3 vector3 = Vector3.left * x + Vector3.down * y;
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }

    private void Animate()
    {
        if (input.magnitude > 0.1f)
        {
            moving = true;
            isWalking = true;
        }
        else
        {
            moving = false;
            isWalking = false;
        }
        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        anim.SetBool("Moving", moving);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        currentHearts -= damage;
        currentHearts = Mathf.Max(0, currentHearts);
        healthUI.SetHearts(currentHearts);

        if (currentHearts <= 0)
        {
            gm.GameOver();
            Destroy(gameObject);
        }
        else
        {
            Invoke(nameof(ResetDamageCooldown), damageCooldown);
        }
    }
    private void ResetDamageCooldown()
    {
        canTakeDamage = true;
    }
}