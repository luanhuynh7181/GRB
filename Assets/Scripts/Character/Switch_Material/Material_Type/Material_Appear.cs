using UnityEngine;

public class Material_Appear : Material_Character
{
    private bool isUpdating = false;
    private float timer;

    public override void RunEffect()
    {
        timer = 0;
        isUpdating = true;
    }

    void Awake()
    {
        material = new Material(Resources.Load<Material>("Shaders/Dissolve/Dissolve_Material"));
        timeRunEffect = EnermiesStat.Ins.timeAppear;
    }

    void Update()
    {
        if (!isUpdating)
            return;
        timer += Time.deltaTime;
        if (timer >= timeRunEffect)
        {
            isUpdating = false;
            return;
        }
        ;
        material.SetFloat("_Amount", Mathf.Clamp01(timer / timeRunEffect));
    }
}
