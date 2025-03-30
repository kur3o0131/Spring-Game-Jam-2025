using UnityEngine;

public class weapon : MonoBehaviour
{
    // set the damage for the weapon
    public float damage = 1;

    // providing the weapon choices
    public enum WeaponType
    {
        Melee,
        Bullet
    }

    // set the weapon type for player
    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if it hit a enemy
        EnemyClass enemy = collision.GetComponent<EnemyClass>();
        if (enemy != null)
        {
            // make the enemy take damage but also destroy weapon if it is a bullet
            enemy.TakeDamage(damage);

            if (weaponType == WeaponType.Bullet)
            {
                Destroy(gameObject);
            }
            return;
        }

        // bullets also need to be destroyed when they hit the boundary
        if (weaponType == WeaponType.Bullet && collision.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
