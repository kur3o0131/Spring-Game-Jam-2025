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

    void onShoot()
    {
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0f;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireforce, ForceMode2D.Impulse);
            Destroy(intBullet, 2f);
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
