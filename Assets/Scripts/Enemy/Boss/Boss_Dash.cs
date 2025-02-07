using System.Collections;
using UnityEngine;

public class Boss_Dash : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float dashingPower = 10f;

    [SerializeField]
    private float dashingTime = 0.2f;

    private bool isDashing = false;
    private Transform player;
    private Rigidbody2D rb;
    private float timer = 0;

    public void Dash()
    {
        boxCollider.enabled = true;
        timer = 0;
        isDashing = true;
        TrailRenderer tr = GetComponent<TrailRenderer>();
        tr.emitting = true;
    }

    private void Awake()
    {
        boxCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FinishDash()
    {
        TrailRenderer tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
        isDashing = false;
        boxCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            return;
        if (!player)
            return;
        timer += Time.fixedDeltaTime;
        if (timer >= dashingTime)
        {
            FinishDash();
            return;
        }
        float distance = player.position.x - rb.position.x;
        if (Mathf.Abs(distance) < 3)
        {
            return;
        }
        Vector2 target = new Vector2(distance, 0).normalized;
        Vector2 newPos = rb.position + target * dashingPower * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        if (!boxCollider.enabled)
            return;
        FinishDash();
        collision
            .gameObject.GetComponent<Player_Controller>()
            ?.DealDame(GameConstants.ENERMY_TYPE.BOSS, transform, false);
    }
}
