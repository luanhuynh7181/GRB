using System.Collections;
using GameConstants;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public HP_Bar_Follower HPBar;
    public TMPro.TextMeshProUGUI text;
    public static int HPCheat = 0;

    void Start()
    {
        HPBar.setMaxHP(PlayerStat.Ins.playerHP + HPCheat);
        HPBar.SetActive(true);
    }

    private void OnEnable()
    {
        GlobalEventManager.OnMushroomEaten += AddHP;
        GlobalEventManager.OnChangeMap += OnChangeMap;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnMushroomEaten -= AddHP;
        GlobalEventManager.OnChangeMap -= OnChangeMap;
    }

    public void AddHP(float percent)
    {
        HPBar.addHP(percent);
        text.text = $"+{percent * 100}% HP";
        StartCoroutine(HideText(2f));
    }

    IEnumerator HideText(float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    public void DealDame(ENERMY_TYPE type, Transform enermy, bool isKnock = false)
    {
        float dame = 0;
        switch (type)
        {
            case ENERMY_TYPE.ARCHER:
            {
                dame = EnermiesStat.Ins.enermyArcherDame;
                break;
            }

            case ENERMY_TYPE.TORCH:
            {
                dame = EnermiesStat.Ins.torchDame;
                break;
            }
            case ENERMY_TYPE.BARREL_BOOM:
            {
                dame = EnermiesStat.Ins.barrelDame;

                break;
            }
            case ENERMY_TYPE.LASER_GUN:
            {
                dame = EnermiesStat.Ins.laserDame;
                break;
            }
            case ENERMY_TYPE.BOSS:
            {
                dame = EnermiesStat.Ins.bossDame;
                break;
            }
            case ENERMY_TYPE.GOBLIN:
            {
                dame = EnermiesStat.Ins.TNTDame;
                break;
            }
        }
        HPBar.ChangeHealth(-dame);
        if (isKnock)
        {
            GetComponent<Player_Movement>()?.Knock(enermy, StatsManager.instance.knockForce);
        }
        GetComponent<Material_Controller>()?.Change(MATERIAL_TYPE.ATTACKED);
        if (HPBar.isDead())
        {
            Destroy(gameObject);
            GlobalEventManager.TriggerShowPopupEndGame(false);
        }
    }

    public void PlayerEnterMountain(bool isEnter)
    {
        string layerName = isEnter
            ? Layer_Collision.Player_Mountain.ToString()
            : Layer_Collision.Player_Ground.ToString();
        gameObject.layer = LayerMask.NameToLayer(layerName);

        Utility.ChangeLayerSorting(gameObject, !isEnter);
    }

    private void OnChangeMap(MAP mapTo)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Destroy(gameObject.GetComponent<Player_Movement>());
        float speed = gameObject.GetComponent<Player_Movement>().speed;
        switch (mapTo)
        {
            case MAP.MAP_0:
            {
                var script = gameObject.AddComponent<Player_Movement_Wave_0>();
                gameObject.layer = (int)Layer_Collision.Player_Ground;
                break;
            }
            case MAP.MAP_1:
            {
                var script = gameObject.AddComponent<Player_Movement_Wave_1>();
                gameObject.layer = (int)Layer_Collision.Player_Ground;
                break;
            }
        }
    }

    public void CheatHP()
    {
        HPCheat += 20;
        HPBar.setMaxHP(PlayerStat.Ins.playerHP + HPCheat);
        text.text = $"Cheat 20 HP Max";
        StartCoroutine(HideText(2f));
    }
}
