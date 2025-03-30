using UnityEngine;

public class weapon : MonoBehaviour
{
    public float damage = 1;

    public enum WeaponType
    {
        Melee,
        Bullet
    }

    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for enemy collision first
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            if (weaponType == WeaponType.Bullet)
            {
                Destroy(gameObject);
            }
            return;
        }

        // If it's a bullet, also destroy it if it hits a boundary
        if (weaponType == WeaponType.Bullet && collision.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
