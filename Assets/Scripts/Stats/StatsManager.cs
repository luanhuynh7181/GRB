using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    public static StatsManager Ins;
    [Header("Common")]
    public float swordRange;
    public float knockForce;
    public float knockTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
