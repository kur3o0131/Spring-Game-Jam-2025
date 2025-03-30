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

    // create an instance of the game manager to call game over when player dies
    private GameManager gm;

    // create an instance for the game object aim to caclulating and using the attack
    public Transform aim;
    bool isWalking = false;

    // create an instance for the animator to animate the player
    public Animator anim;

    // variable to set the player move speed
    public float moveSpeed;

    // create an instance for the rigidbody to apply 2d phshycsi
    private Rigidbody2D rb;

    // // variables to get the input of player movement and direction
    private float x;
    private float y;
    private Vector2 input;
    private bool moving;

    void Start()
    {
        // set values for when player is spawned every round
        currentHearts = maxHearts;
        healthUI.SetHearts(currentHearts);
        rb = GetComponent<Rigidbody2D>();
        gm = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // update inputs, direction, and animation of player
        GetInput();
        GetComponent<attack>()?.UpdateFacing(input);
        Animate();
    }

    private void FixedUpdate()
    {
        // move the player according to the input
        rb.linearVelocity = input * moveSpeed;
        // rotate the player according to the input (up down left or right)
        if (isWalking)
        {
            Vector3 vector3 = Vector3.left * x + Vector3.down * y;
            aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    private void GetInput()
    {
        // getting the input for default keybinds 
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, y).normalized;
    }

    private void Animate()
    {
        // animate the player using the x and y + wether player is moving
        moving = input.magnitude > 0.1f;
        isWalking = moving;

        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        anim.SetBool("Moving", moving);
    }

    // taking damage if the player walks into an enemy
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

        // decreasing the hearts and updating the health ui
        canTakeDamage = false;
        currentHearts -= damage;
        currentHearts = Mathf.Max(0, currentHearts);
        healthUI.SetHearts(currentHearts);
        // sound when player takes damage
        if (sfxsource && damageSFX)
            sfxsource.PlayOneShot(damageSFX, damageVolume);
        // calling the gameover canvas when player dies
        if (currentHearts <= 0)
        {
            gm.GameOver();
            Destroy(gameObject);
        }
        else
        { // cool down for taking damage
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
