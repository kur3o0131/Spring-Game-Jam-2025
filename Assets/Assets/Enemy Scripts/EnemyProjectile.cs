using UnityEngine;

public class Projectile : MonoBehaviour
{
    // set a speed for the projectile and how much damage it will do
    public float speed = 10f;
    public float damage = 1f;
    // also create vector for the direction of the projectile will go
    private Vector2 direction;
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
    void Start()
    {
        // destroy the projectile after 3 seconds no matter what
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy the projectile if it hits a boundary
        if (collision.CompareTag("Boundary") || collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
            return;
        }

        // ignore bullets that go thru other enemies
        if (collision.GetComponent<EnemyClass>())
            return;

        // make the player take damage if the projectile hits the player
        if (collision.CompareTag("Player"))
        {
            PlayerCntrl player = collision.GetComponent<PlayerCntrl>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
