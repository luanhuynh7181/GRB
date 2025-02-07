using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Change_Enrage : StateMachineBehaviour
{
    [SerializeField]
    private bool isEnrage = false;

    private Transform player;
    private Rigidbody2D rb;

    [SerializeField]
    private float time;

    [Header("Enrage when player jump over boss")]
    [SerializeField]
    private float timeEnrage = 0f;

    private float timer;
    private float timerOutEnrage = 0;

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
        if (!isEnrage)
        {
            timerOutEnrage = timeEnrage;
        }
    }

    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (!player)
            return;
        timer += Time.deltaTime;
        if (timer > time)
        {
            animator.SetBool("IsEnraged", !isEnrage);
            timer = 0;
        }
        if (!isEnrage && player.position.y > rb.position.y)
        {
            timerOutEnrage -= Time.deltaTime;
            if (timerOutEnrage < 0)
            {
                animator.SetBool("IsEnraged", true);
            }
        }
    }
}
