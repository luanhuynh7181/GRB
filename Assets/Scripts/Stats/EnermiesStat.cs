using UnityEngine;

public class EnermiesStat : MonoBehaviour
{ // StatsManager.instance
    public static EnermiesStat Ins;

    public float timeAppear;
    public float timeDeath;

    [Header("Torch")]
    public int torchDame;
    public int torchHP;
    public float torchRangeAttack;
    public float torchRangeChasing;
    public float torchCoolDown;
    public float torchSpeed;

    [Header("TNT")]
    public int TNTDame;
    public int TNT_HP;
    public float TNTRangeAttack;
    public float TNTRangeChasing;
    public float TNTCoolDown;
    public float TNTSpeed;

    [Header("EnermyArcher")]
    public int enermyArcherDame;
    public int enermyArcherHP;
    public float emermyArcherRangeChasing;
    public float enermyArcherCoolDown;

    [Header("Barrel")]
    public int barrelHP;
    public int barrelDame;
    public float barrelRangeChasing;
    public float barrelCoolDown;
    public int barrelBoomAppear;

    [Header("Laser")]
    public float laserDame;
    public float laserSpeed;
    public float laserMaxWidth;
    public float timeRest;
    public float timeActive;

    [Header("BOSS")]
    public float bossDame;
    public int bossHP;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
