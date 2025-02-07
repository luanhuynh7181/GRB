using Unity.VisualScripting;
using UnityEngine;

public class Laser_Gun_Dynamic : Laser_Gun
{
    private int dir = 1;
    private float offsetRotate = 70;
    private float speedMove;

    public override void Init()
    {
        speedMove = EnermiesStat.Ins.laserSpeed;
    }

    public void RoatateCluster()
    {
        float currentZ = cluster.transform.rotation.eulerAngles.z;
        if (currentZ > 180)
            currentZ -= 360;

        float addition = dir * speedMove * Time.deltaTime;
        float newZ = currentZ + addition;
        if (newZ > offsetRotate)
        {
            newZ = offsetRotate;
            dir *= -1;
        }
        else if (newZ < -offsetRotate)
        {
            newZ = -offsetRotate;
            dir *= -1;
        }
        cluster.transform.rotation = Quaternion.Euler(0, 0, newZ);
    }

    void LateUpdate()
    {
        if (state != STATE.OutRange)
        {
            RoatateCluster();
            UpdateLiner(dirLine.transform.position);
        }
    }
}
