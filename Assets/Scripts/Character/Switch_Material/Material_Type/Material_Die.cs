using UnityEngine;
using UnityEngine.UIElements;

public class Material_Die : Material_Character
{
    private bool isUpdating = false;
    private float maxProgress = 0.04f;
    private float timer;

    public override void RunEffect()
    {
        timer = 0;
        isUpdating = true;
    }

    void Awake()
    {
        material = new Material(Resources.Load<Material>("Shaders/Blur/Blur_Material"));
        material.SetFloat("_Max", maxProgress);
        timeRunEffect = EnermiesStat.Ins.timeDeath;
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
        material.SetFloat("_Amount", timer / timeRunEffect * maxProgress);
    }
}
