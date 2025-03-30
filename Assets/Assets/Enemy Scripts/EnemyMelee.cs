using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeEnemy : EnemyClass
{
    // instantiate a animator for the melee enemy
    public Animator anim;

    // override start method from base class
    protected override void Start()
    {
        // call the start method from the base class
        base.Start();
        // melee will be able to take 3 hit points
        health = 3f;

        // if the animator is null, get the animator component
        if (anim == null)
            anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        // if there is no target = player is dead
        if (!target) return;

        // moving towards the player
        Vector2 direction = (target.position - transform.position).normalized;
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (anim)
        {
            // updating the animation parameters, to know which to use
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
            anim.SetBool("Moving", direction.magnitude > 0.1f);
        }
    }

}
