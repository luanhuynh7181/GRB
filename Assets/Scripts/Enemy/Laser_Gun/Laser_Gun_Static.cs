using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Gun_Static : Laser_Gun
{
    private float lasX = -1;
    private float offset = 0.03f;
    private Transform player;
    private Vector3 predictionPos;
    private Vector2 rangeOffset = new Vector2(-5f, 5f);
    private float threshold = 0;

    public override void OnTriggerPlayer(GameObject player)
    {
        this.player = player.transform;
    }

    public void RoatateCluster(Vector3 player)
    {
        Vector3 direction = player - cluster.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cluster.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    protected override void ChangeState(STATE newState)
    {
        base.ChangeState(newState);
        if (newState == STATE.CoolDown)
        {
            predictionPos = player.position;
        }
    }

    void LateUpdate()
    {
        if (state == STATE.CoolDown)
        {
            if (!player)
                return;
            float currentX = player.transform.position.x;
            float delta = currentX - lasX;
            lasX = currentX;
            if (delta > 0)
            {
                threshold += offset;
            }
            else if (delta < 0)
            {
                threshold -= offset;
            }
            else
            {
                threshold *= 0.95f;
            }

            threshold = Mathf.Clamp(threshold, rangeOffset.x, rangeOffset.y);
            Vector3 prediction = player.position + Vector3.right * threshold;
            RoatateCluster(prediction);
            UpdateLiner(prediction);
        }
    }
}
