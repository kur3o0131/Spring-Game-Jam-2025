using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeEnemy : EnemyBase
{
    public Animator anim;

    protected override void Start()
    {
        base.Start();
        health = 3f;

        if (anim == null)
            anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!target) return;

        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 newPos = rb.position + dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        if (anim)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
            anim.SetBool("Moving", dir.magnitude > 0.1f);
        }
    }

}
