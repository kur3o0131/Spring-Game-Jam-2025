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

    void RotateAimToMouse() // <-- new method
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
            Vector3 spawnPos = Aim.position + Aim.up * 0.6f;
            GameObject intBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            Rigidbody2D rb = intBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Aim.up * fireforce, ForceMode2D.Impulse);
            }

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
