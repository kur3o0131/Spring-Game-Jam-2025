using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    public Transform Aim;
    public GameObject bullet;
    public float fireforce = 10f;

    public float shootCooldown = 0.5f;
    private float shootTimer = 0f;

    public Animator anim;

    private Vector2 playerdirection = Vector2.right;

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - Aim.position).normalized;
        Aim.up = direction;
    }

    void onShoot()
    {
        Vector3 spawnPos = Aim.position + Aim.up * 0.6f;
        GameObject intBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

        Rigidbody2D rb = intBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - Aim.position).normalized;
            rb.linearVelocity = direction * fireforce;
        }

        Destroy(intBullet, 3f);
    }
    void OnAttack()
    {
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
