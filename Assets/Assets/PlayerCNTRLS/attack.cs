using UnityEngine;

public class attack : MonoBehaviour
{
    // for melee attacks
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    // for range attacks
    public Transform Aim;
    public GameObject bullet;
    public float fireforce = 10f;
    public float shootCooldown = 0.5f;
    private float shootTimer = 0f;

    // added animator for attack animation, only added for melee
    public Animator anim;

    // getting the players direction
    private Vector2 playerdirection = Vector2.right;

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        // constantly checking mouse position + whether an input has been made ( left click or right click) 
        RotateAimToMouse();
        CheckMeleeTimer();
        shootTimer += Time.deltaTime;

        // melee attack
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            GetComponent<PlayerCntrl>()?.PlayPunchSound();
            OnAttack();
        }

        // ranged attack (with cooldown)
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1)) && shootTimer >= shootCooldown)
        {
            shootTimer = 0f;
            GetComponent<PlayerCntrl>()?.PlayShootSound();
            onShoot();
        }
    }

    void RotateAimToMouse()
    {
        // get the mouse position and direction to aim
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - Aim.position).normalized;
        Aim.up = direction;
    }
    void onShoot()
    {
        // using the mouse position we receive to spawn the bullet in that direction
        Vector3 spawnPos = Aim.position + Aim.up * 0.6f;
        GameObject intBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

        Rigidbody2D rb = intBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // applying force to the bullet to move towards the mouse at its current position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - Aim.position).normalized;
            rb.linearVelocity = direction * fireforce;
        }
        // destroy teh bullet after 3 seconds no matter wat
        Destroy(intBullet, 3f);
    }
    void OnAttack()
    {
        // triggering the animation and activting an attack using the melee position
        if (!isAttacking)
        {
            UpdateMeleePosition();
            Melee.SetActive(true);
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
    }
    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            // uses real time to check how long the attack has been active
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                Melee.SetActive(false);
                isAttacking = false;
                atkTimer = 0f;
            }
        }
    }
    // update the direction of the player 
    public void UpdateFacing(Vector2 moveInput)
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            playerdirection = moveInput.normalized;
        }
    }
    // getting the melee attack based on position not mouse
    void UpdateMeleePosition()
    {
        Melee.transform.position = transform.position + (Vector3)(playerdirection * 0.6f);
        Melee.transform.up = playerdirection;
    }

}
