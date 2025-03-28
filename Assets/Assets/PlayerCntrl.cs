using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCntrl : MonoBehaviour
{
    public Animator anim;

    public float moveSpeed;

    private Rigidbody2D rb;

    private float x;
    private float y;

    private Vector2 input;
    private bool moving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }

    private void Animate()
    {
        if (input.magnitude > 0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        anim.SetBool("Moving", moving);
    }
}