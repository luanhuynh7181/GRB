using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Material_Attacked : Material_Character
{
    public override void RunEffect()
    {
        TurnIntoRed();
        Invoke("TurnIntoOrigin", 0.1f);
        Invoke("TurnIntoRed", 0.2f);
        Invoke("TurnIntoOrigin", 0.3f);
        Invoke("TurnIntoRed", 0.4f);
        Invoke("TurnIntoOrigin", 0.5f);
    }

    void Awake()
    {
        material = new Material(Resources.Load<Material>("Shaders/Attacked/Attacked_Material"));
        timeRunEffect = 0.5f;
    }

    private void TurnIntoOrigin()
    {
        material.color = Color.white;
    }

    private void TurnIntoRed()
    {
        material.color = Color.red;
    }
}
