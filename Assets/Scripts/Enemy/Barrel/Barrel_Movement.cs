using GameConstants;
using UnityEngine;

public class Barrel_Movement : Enermy_Movement
{
    private float radius = 4f;

    public override void Attack()
    {
        Vector2[] directions = GetSurroundingDirections(EnermiesStat.Ins.barrelBoomAppear, radius);
        float ofssetPos = 1.5f;
        foreach (Vector2 direction in directions)
        {
            Vector2 target =
                (Vector2)transform.position
                + direction
                + new Vector2(
                    Random.Range(-ofssetPos, ofssetPos),
                    Random.Range(-ofssetPos, ofssetPos)
                );

            GameObject obj = ObjectPoolManager.Instance.GetFromPool(
                GameConstants.POOL_TYPE.BARREL_BOOM
            );
            obj.transform.position = transform.position;
            Barrel_Boom boom = obj.GetComponent<Barrel_Boom>();

            boom.Init(target, Random.Range(3, 5), Random.Range(3, 5));
        }
    }

    public override void ChangeVelocity(Vector2 dir) { }

    public override void CheckUpdate()
    {
        base.CheckUpdate();
        if (player == null)
            return;
        LookAtPlayer();
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
        rangeChasing = EnermiesStat.Ins.barrelRangeChasing;
        rangeAttack = rangeChasing;
        coolDown = EnermiesStat.Ins.barrelCoolDown;
        canAttackFromMountain = true;
    }

    private Vector2[] GetSurroundingDirections(int numDirections, float radius)
    {
        Vector2[] directions = new Vector2[numDirections];
        float angleStep = 360f / numDirections;

        for (int i = 0; i < numDirections; i++)
        {
            float angle = angleStep * i;
            float radian = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;

            directions[i] = new Vector2(x, y);
        }

        return directions;
    }
}
