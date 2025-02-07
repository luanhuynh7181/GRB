using UnityEngine;

public class Enermy_Controller : MonoBehaviour
{
    public HP_Bar_Follower HPBar;
    public bool isGround = true;

    public void Active()
    {
        HPBar.SetActive(true);
    }

    public virtual void Init()
    {
        gameObject.layer = (int)(
            isGround ? Layer_Collision.Enermy_Ground : Layer_Collision.Enermy_Mountain
        );
        Utility.ChangeLayerSorting(gameObject, isGround);
    }

    public virtual void PlayerAttack(int dame, Transform player)
    {
        var scriptMovement = GetComponent<Enermy_Movement>();
        if (scriptMovement && scriptMovement.isHiding())
        {
            return;
        }
        HPBar.ChangeHealth(-dame);

        if (HPBar.isDead())
        {
            HPBar.SetActive(false);
            GetComponent<Enermy_Movement>()?.ChangeStateToDie();
            GetComponent<Material_Controller>()?.Change(GameConstants.MATERIAL_TYPE.DIE);
        }
        else
        {
            GetComponent<Material_Controller>()?.Change(GameConstants.MATERIAL_TYPE.ATTACKED);
            GetComponent<Enermy_Knocked>()
                ?.KnockBack(
                    player,
                    StatsManager.instance.knockForce,
                    StatsManager.instance.knockTime
                );
        }
    }

    void Start()
    {
        Init();
    }
}
