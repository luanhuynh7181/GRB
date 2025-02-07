using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using GameConstants;
using UnityEngine;

public class Goblin_Movement : Enermy_Movement
{
    public Transform attackPoint;
    protected Node_Tile lastPlayerPos = null;
    protected List<Node_Tile> path = new List<Node_Tile>();

    public override void ChangeVelocity(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            rb.linearVelocity = dir;
            return;
        }
        //run A Star in ground
        if (
            gameObject.layer == ((int)Layer_Collision.Enermy_Ground)
            && state == CHARACTER_STATE.CHASE
        )
        {
            CheckUpdateDestinationPos();
            if (path != null && path.Count > 0)
            {
                dir = (path.First().GetPosition() - (Vector2)transform.position).normalized * speed;
            }
            else
            {
                dir = (player.transform.position - transform.position).normalized * speed;
            }
        }
        rb.linearVelocity = dir;
    }

    public override string getNameState(CHARACTER_STATE state)
    {
        switch (state)
        {
            case CHARACTER_STATE.IDLE:
                return "isIdle";
            case CHARACTER_STATE.CHASE:
                return "isWalking";
            case CHARACTER_STATE.ATTACK:
                return "isAttacking";
            case CHARACTER_STATE.TEST:
                return "isTest";
            case CHARACTER_STATE.KNOCK:
                return "isIdle";
        }
        return base.getNameState(state);
    }

    private void CheckUpdateDestinationPos()
    {
        if (DevMode.IsOffAStar)
            return;
        if (player == null)
            return;

        Vector2 cur = transform.position;
        Vector2 target = player.position;

        Node_Tile playerPos = new Node_Tile(target.x, target.y);
        Node_Tile curPos = new Node_Tile(cur.x, cur.y);
        if (!playerPos.Equals(lastPlayerPos))
        {
            lastPlayerPos = playerPos;
            path = AStar.FindPath(curPos, playerPos);
            return;
        }

        if (path != null && path.Count > 0)
        {
            float offset = 0.1f;
            bool isSameTile =
                Vector2.Distance(path.First().GetPosition(), curPos.GetPosition()) < offset;
            if (isSameTile)
            {
                path.RemoveAt(0);
            }
        }
    }
}
