using UnityEngine;

public class Barrel_Controller : Enermy_Controller
{
    public override void Init()
    {
        base.Init();
        HPBar.setMaxHP(EnermiesStat.Ins.barrelHP);
    }
}
