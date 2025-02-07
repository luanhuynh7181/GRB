using UnityEngine;

public class TNT_Controller : Enermy_Controller
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Init()
    {
        base.Init();
        HPBar.setMaxHP(EnermiesStat.Ins.TNT_HP);
    }
}
