using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCntrl : MonoBehaviour
{
    public AudioSource sfxsource;
    public AudioClip punchSFX;
    [Range(0f, 1f)] public float punchVolume = 0.5f;
    public AudioClip shootSFX;
    [Range(0f, 1f)] public float shootVolume = 0.5f;
    public AudioClip damageSFX;
    [Range(0f, 1f)] public float damageVolume = 0.5f;

    // referce to the ui for hp
    public HealthUI healthUI;
    // player has 3 hearts
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

    void Start()
    {
        currentHearts = maxHearts;
        healthUI.SetHearts(currentHearts);
        rb = GetComponent<Rigidbody2D>();
        gm = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;

        if (isWalking)
        {
            Vector3 vector3 = Vector3.left * x + Vector3.down * y;
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y).normalized;
    }

    private void Animate()
    {
        moving = input.magnitude > 0.1f;
        isWalking = moving;

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

        if (sfxsource && damageSFX)
            sfxsource.PlayOneShot(damageSFX, damageVolume);

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
    // playing sounds according to weapon and volume
    public void PlayPunchSound()
    {
        if (sfxsource && punchSFX)
            sfxsource.PlayOneShot(punchSFX, punchVolume);
    }
    public void PlayShootSound()
    {
        if (sfxsource && shootSFX)
            sfxsource.PlayOneShot(shootSFX, shootVolume);
    }
}
