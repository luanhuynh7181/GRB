using GameConstants;
using UnityEngine;

public class Player_Change_Equipment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PLAYER_TYPE type;

    void Start()
    {
        type = PLAYER_TYPE.WARRIOR;
        UpdateForWarrior();
    }

    private void SwitchPlayer()
    {
        if (type == PLAYER_TYPE.ARCHER)
        {
            type = PLAYER_TYPE.WARRIOR;
            UpdateForWarrior();
        }
        else
        {
            type = PLAYER_TYPE.ARCHER;
            UpdateForArcher();
        }

        GetComponent<Player_Movement>().UpdateSpeed();
    }

    private void UpdateForWarrior()
    {
        GetComponent<Player_Archer>().enabled = false;
        GetComponent<Player_Warrior>().enabled = true;
    }

    private void UpdateForArcher()
    {
        GetComponent<Player_Archer>().enabled = true;
        GetComponent<Player_Warrior>().enabled = false;
    }

    public float GetSpeedPlayer()
    {
        if (type == PLAYER_TYPE.WARRIOR)
        {
            return PlayerStat.Ins.warriorSpeed;
        }
        else
        {
            return PlayerStat.Ins.archerSpeed;
        }
    }

    void Update()
    {
        if (InputHandler.GetInstance().IsChangeEquipment())
        {
            SwitchPlayer();
        }
    }
}
