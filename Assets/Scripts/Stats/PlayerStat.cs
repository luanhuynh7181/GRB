using UnityEngine;

public class PlayerStat : MonoBehaviour
{// StatsManager.instance
    public static PlayerStat Ins;

    public int playerHP;

    [Header("Warrior")]
    public int warriorDame;
    public float warriorSpeed;
    public float warriorCooldown;

    [Header("Archer")]
    public int archerDame;
    public float archerSpeed;
    public float archerCooldown;

    [Header("Waves")]
    public float ratioSpeed;
    public int extraJump;
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
