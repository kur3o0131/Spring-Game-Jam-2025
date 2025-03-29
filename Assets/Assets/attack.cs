using UnityEngine;

public class attack : MonoBehaviour
{
    // melee attack
    public GameObject Melee;
    bool isAttacking = false;

    float atkDuration = 0.3f;
    float atkTimer = 0f;

    // ranged attack
    public Transform Aim;
    public GameObject bullet;
    public float fireforce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;

    public Animator anim;

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        RotateAimToMouse(); // <-- new line

        CheckMeleeTimer();
        shootTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            OnAttack();
        }

        if (Input.GetKeyUp(KeyCode.R) || Input.GetMouseButton(1))
        {
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
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0f;

            // Position the bullet slightly in front of the player
            Vector3 spawnPos = Aim.position + Aim.up * 0.6f;
            GameObject intBullet = Instantiate(bullet, spawnPos, Quaternion.identity);

            Rigidbody2D rb = intBullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Set the Rigidbody2D to Kinematic (no physics influence)
                rb.bodyType = RigidbodyType2D.Kinematic;

                // Get the mouse position in world space
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Calculate direction towards the mouse (normalize it so it's a unit vector)
                Vector2 direction = (mousePos - Aim.position).normalized;

                // Apply the linearVelocity for constant speed
                rb.linearVelocity = direction * fireforce;
            }

            // Destroy the bullet after 3 seconds
            Destroy(intBullet, 3f);
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
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
}
