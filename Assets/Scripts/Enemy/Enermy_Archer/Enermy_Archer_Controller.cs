using UnityEngine;

public class Enermy_Archer_Controller : Enermy_Controller
{
    public override void Init()
    {
        base.Init();
        HPBar?.setMaxHP(EnermiesStat.Ins.enermyArcherHP);

    }


}
