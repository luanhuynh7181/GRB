using System;
using UnityEngine;

public class Boss_Controller : Enermy_Controller
{
    private Transform player;

    // todo hideBoss
    public override void Init()
    {
        gameObject.layer = (int)(
            isGround ? Layer_Collision.Enermy_Ground : Layer_Collision.Enermy_Mountain
        );
        HPBar.setMaxHP(EnermiesStat.Ins.bossHP);
    }

    public override void PlayerAttack(int dame, Transform player)
    {
        HPBar.ChangeHealth(-dame);

        if (HPBar.isDead())
        {
            HPBar.SetActive(false);
            GlobalEventManager.TriggerShowPopupEndGame(true);
        }
    }

    protected void LookAtPlay()
    {
        if (!player)
            return;
        float distance = player.position.x - transform.position.x;
        if (Mathf.Abs(distance) < 0.1f)
            return;
        if (distance * transform.localScale.x <= 0)
            return;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Animator>()?.Play("Intro");
        HPBar.SetActive(true);
    }

    void Update()
    {
        LookAtPlay();
    }
}
