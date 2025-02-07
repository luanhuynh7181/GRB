using System.Linq;
using GameConstants;
using UnityEngine;

public class TNT_Movement : Goblin_Movement
{
    public override void Init()
    {
        speed = EnermiesStat.Ins.TNTSpeed;
        rangeChasing = EnermiesStat.Ins.TNTRangeChasing;
        rangeAttack = EnermiesStat.Ins.TNTRangeAttack;
        coolDown = EnermiesStat.Ins.TNTCoolDown;
    }

    public override void Attack()
    {
        GameObject obj = ObjectPoolManager.Instance.GetFromPool(GameConstants.POOL_TYPE.DYNAMITE);
        obj.transform.position = transform.position;
        Dynamite boom = obj.GetComponent<Dynamite>();
        boom.Init(player.transform.position, Random.Range(3, 5));
    }
}
