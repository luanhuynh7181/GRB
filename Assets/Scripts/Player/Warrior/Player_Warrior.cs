using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player_Warrior : MonoBehaviour
{
    private Animator anim;
    private float timer = 0f;
    public Transform attackPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (InputHandler.GetInstance().IsAttack())
        {
            Attack();
        }
    }

    public void OnEnable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetBool("isShooting", false);
        anim.SetLayerWeight(1, 0);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.6f, 1f);
    }

    public void Attack()
    {
        if (timer > 0)
            return;
        timer = PlayerStat.Ins.warriorCooldown;
        anim.SetBool("isAttacking", true);
    }

    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
        LayerMask? enemyLayer = LayerUtils.GetEnermyLayerByPlayer(gameObject);
        if (enemyLayer == null)
            return;
        Collider2D[] enermys = Physics2D.OverlapCircleAll(
            attackPoint.position,
            StatsManager.instance.swordRange,
            1 << enemyLayer.Value
        );
        foreach (Collider2D enermy in enermys)
        {
            enermy
                .GetComponent<Enermy_Controller>()
                ?.PlayerAttack(PlayerStat.Ins.warriorDame, transform);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPoint.position, 0.8f);
    //}
}
