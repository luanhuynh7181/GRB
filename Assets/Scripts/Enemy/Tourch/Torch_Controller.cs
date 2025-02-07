using UnityEngine;

public class Torch_Controller : Enermy_Controller
{
    public override void Init()
    {
        base.Init();
        HPBar.setMaxHP(EnermiesStat.Ins.torchHP);
    }


}
