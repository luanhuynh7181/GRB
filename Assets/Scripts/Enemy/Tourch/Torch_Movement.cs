using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using GameConstants;
using UnityEngine;

public class Torch_Movement : Goblin_Movement
{
    public override void Init()
    {
        speed = EnermiesStat.Ins.torchSpeed;
        rangeChasing = EnermiesStat.Ins.torchRangeChasing;
        rangeAttack = EnermiesStat.Ins.torchRangeAttack;
        coolDown = EnermiesStat.Ins.torchCoolDown;
    }

    public override void Attack()
    {
        LayerMask? playerLayer = LayerUtils.GetPlayerLayerByEnermy(gameObject);
        if (playerLayer == null)
            return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            EnermiesStat.Ins.torchRangeAttack,
            1 << playerLayer.Value
        );
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<Player_Controller>()?.DealDame(ENERMY_TYPE.TORCH, transform, true);
            break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!attackPoint)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 1.8f);
    }
}
