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
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            if (weaponType == WeaponType.Bullet)
            {
                Destroy(gameObject);
            }
        }
    }
}
