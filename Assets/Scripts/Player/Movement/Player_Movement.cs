using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    protected Animator anim;
    protected bool isKnock = false;
    protected Rigidbody2D rb;
    protected float ratioSpeed = 1;

    public void Knock(Transform enemy, float force)
    {
        isKnock = true;
        Vector2 dir = (transform.position - enemy.position).normalized * force;
        rb.linearVelocity = dir;
        StartCoroutine(KnockCo());
    }

    protected virtual void CheckInputMovement() { }

    protected void FlipPlayer()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected virtual void Init() { }

    IEnumerator KnockCo()
    {
        yield return new WaitForSeconds(StatsManager.instance.knockTime);
        rb.linearVelocity = Vector2.zero;
        isKnock = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateSpeed();
        Init();
    }

    void Update()
    {
        CheckInputMovement();
    }

    public float GetRatioSpeed()
    {
        return ratioSpeed;
    }

    public void UpdateSpeed()
    {
        speed = GetComponent<Player_Change_Equipment>().GetSpeedPlayer();
    }
}
