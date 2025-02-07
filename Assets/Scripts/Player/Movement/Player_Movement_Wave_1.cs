using System;
using System.Drawing;
using UnityEngine;

public class Player_Movement_Wave_1 : Player_Movement
{
    [SerializeField]
    private float fallMultiplier = 3f;

    [SerializeField]
    private LayerMask groundLayer;

    private bool isGround = false;
    private bool isHighJump = false;
    private float jumpCounter;

    [SerializeField]
    private float jumpForce = 13f;

    private int jumpLeft;

    [SerializeField]
    private float jumpMultiplier = 1f;

    [SerializeField]
    private float timeJump = 0.2f;

    private Vector2 vecGravity;

    [SerializeField]
    private LayerMask wallLayer;

    protected override void CheckInputMovement()
    {
        CheckJump();
        CheckMove();
    }

    protected override void Init()
    {
        base.Init();
        vecGravity = Physics2D.gravity;
        GetComponent<Rigidbody2D>().gravityScale = 2;
        wallLayer = LayerMask.GetMask("Wall");
        groundLayer = LayerMask.GetMask("Ground", "Stair");
        ratioSpeed = PlayerStat.Ins.ratioSpeed;
        InputHandler.GetInstance().SwitchWave(1);
    }

    private void CheckJump()
    {
        Vector2 velocity = rb.linearVelocity;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool canJump = false;
            if (isGround)
            {
                isGround = false;
                canJump = true;
            }
            else
            {
                if (jumpLeft > 0)
                {
                    jumpLeft--;
                    canJump = true;
                }
            }
            if (canJump)
            {
                jumpCounter = 0;
                velocity = new Vector2(velocity.x, jumpForce);
                isHighJump = true;
            }
        }
        if (velocity.y > 0 && isHighJump)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > timeJump)
            {
                isHighJump = false;
            }
            float percent = jumpCounter / timeJump;
            float currentJumpM = jumpMultiplier;

            if (jumpCounter > timeJump / 2)
            {
                currentJumpM *= (1 - jumpCounter / timeJump);
            }
            Vector2 pull = vecGravity * currentJumpM * Time.deltaTime;
            velocity += pull;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isHighJump = false;
            jumpCounter = 0;

            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }
        float lowFall = -30;
        if (velocity.y < 0 && vecGravity.y > lowFall)
        {
            velocity += vecGravity * fallMultiplier * Time.deltaTime;
        }
        rb.linearVelocity = velocity;
    }

    private void CheckMove()
    {
        float horizontal = InputHandler.GetInstance().GetHorizontal();
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            float dir = horizontal > 0 ? 1 : -1;
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                0.1f,
                Vector2.right * dir,
                0.4f,
                wallLayer
            );
            if (hit)
                horizontal = 0;
        }
        if (horizontal * transform.localScale.x < 0)
        {
            FlipPlayer();
        }

        rb.linearVelocity = new Vector2(horizontal * speed * ratioSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.tag;
        if (name.Equals("Ground") || name.Equals("Boss"))
        {
            //Vector2 colliderPoint = collision.GetContact(0).point;
            RaycastHit2D hit = Physics2D.CircleCast(
                (Vector2)transform.position + Vector2.down * 1.2f,
                0.2f,
                Vector2.down,
                0,
                groundLayer
            );
            if (hit.collider)
            {
                isGround = true;
                jumpLeft = PlayerStat.Ins.extraJump;
                jumpCounter = 0;
            }
        }
    }

    private void OnEnable()
    {
        GetComponent<Animator>()?.SetFloat("vertical", 0);
        GetComponent<Rigidbody2D>().gravityScale = 2;
    }
}
