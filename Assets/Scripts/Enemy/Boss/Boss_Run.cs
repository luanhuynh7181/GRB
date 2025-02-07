using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField]
    private float attackRange = 3f;

    private Transform player;
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 2.5f;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        rb = animator.GetComponent<Rigidbody2D>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (!player)
            return;
        if (Vector2.Distance(player.position, rb.position) < attackRange)
        {
            animator.SetTrigger("Attack");
            return;
        }
        Vector2 target = new Vector2(player.position.x - rb.transform.position.x, 0).normalized;
        Vector2 newPos = rb.position + target * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}
