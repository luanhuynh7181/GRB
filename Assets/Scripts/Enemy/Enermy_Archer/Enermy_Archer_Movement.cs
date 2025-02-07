using GameConstants;
using UnityEngine;

public class Enermy_Archer_Movement : Enermy_Movement
{
    public GameObject arrowPrefab;
    private Vector2 dir = Vector2.zero;

    public override void Attack()
    {
        if (state == CHARACTER_STATE.IDLE)
            return;
        ChangeState(CHARACTER_STATE.IDLE);
        if (!IsSameLayer())
            return;
        if (player == null)
            return;

        GameObject obj = ObjectPoolManager.Instance.GetFromPool(GameConstants.POOL_TYPE.ARROW);
        Arrow arrow = obj.GetComponent<Arrow>();
        obj.transform.position = transform.position;
        Layer_Collision layerCollision =
            gameObject.layer == (int)Layer_Collision.Enermy_Ground
                ? Layer_Collision.Enemy_Weapon_Ground
                : Layer_Collision.Enemy_Weapon_Mountain;

        arrow.Init(dir, OnArrowCollision, (int)layerCollision, GetComponent<SpriteRenderer>());
    }

    public override void ChangeVelocity(Vector2 dir) { }

    public override void CheckUpdate()
    {
        HandleAiming();
        base.CheckUpdate();
    }

    public override string getNameState(CHARACTER_STATE state)
    {
        switch (state)
        {
            case CHARACTER_STATE.IDLE:
                return "isIdle";
            case CHARACTER_STATE.ATTACK:
                return "isAttacking";
        }
        return base.getNameState(state);
    }

    public override void Init()
    {
        speed = 0;
        canAttackFromMountain = true;
        rangeChasing = EnermiesStat.Ins.emermyArcherRangeChasing;
        rangeAttack = rangeChasing;
        coolDown = EnermiesStat.Ins.enermyArcherCoolDown;
    }

    public void OnArrowCollision(Collision2D collision)
    {
        collision
            .gameObject.GetComponent<Player_Controller>()
            ?.DealDame(ENERMY_TYPE.ARCHER, transform);
    }

    private void HandleAiming()
    {
        if (player == null)
            return;
        LookAtPlayer();
        dir = (player.position - transform.position).normalized;
        anim.SetFloat("aimX", (dir.x));
        anim.SetFloat("aimY", (dir.y));
    }
}
