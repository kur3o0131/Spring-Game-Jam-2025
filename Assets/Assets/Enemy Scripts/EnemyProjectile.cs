using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't hurt enemies (including the one that fired it)
        if (collision.GetComponent<EnemyBase>()) return;

        if (collision.CompareTag("Player"))
        {
            // Get the PlayerCntrl component on the player
            PlayerCntrl player = collision.GetComponent<PlayerCntrl>();
            if (player != null)
            {
                player.TakeDamage(1);  // Apply damage to the player
            }
            Destroy(gameObject);  // Destroy the projectile
        }

        // Optional: Destroy on walls or anything else (like obstacles)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
