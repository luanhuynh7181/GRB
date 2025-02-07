using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GameConstants;
using Unity.VisualScripting;
using UnityEngine;

public class Material_Controller : MonoBehaviour
{
    private Dictionary<MATERIAL_TYPE, Material_Character> cmd;
    private Material_Character last = null;

    [SerializeField]
    private Material normal;

    private SpriteRenderer rend;
    private Coroutine timeoutCoroutine;

    public void Change(MATERIAL_TYPE type)
    {
        if (DevMode.IsOffShader)
            return;
        if (!cmd.ContainsKey(type))
        {
            Debug.LogError("Material type not found" + type + " " + cmd[type]);
            return;
        }
        if (last != null)
        {
            last.enabled = false;
        }
        last = cmd[type];
        last.enabled = true;
        rend.material = last.material;
        last.RunEffect();
        ResetRender(last.timeRunEffect);
    }

    private void AddMaterial(MATERIAL_TYPE type, string script)
    {
        Material_Character bComponent = (Material_Character)
            gameObject.AddComponent(System.Type.GetType(script));
        cmd.TryAdd(type, bComponent);
    }

    private void ResetRender(float delay)
    {
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
        }
        timeoutCoroutine = StartCoroutine(TimeoutCoroutine(delay));
    }

    void Start()
    {
        if (DevMode.IsOffShader)
            return;
        rend = GetComponent<SpriteRenderer>();
        cmd = new Dictionary<MATERIAL_TYPE, Material_Character>();
        AddMaterial(MATERIAL_TYPE.ATTACKED, typeof(Material_Attacked).Name);
        AddMaterial(MATERIAL_TYPE.DIE, typeof(Material_Die).Name);
        AddMaterial(MATERIAL_TYPE.APPEAR, typeof(Material_Appear).Name);
    }

    private IEnumerator TimeoutCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        timeoutCoroutine = null;
        last.enabled = false;
        rend.material = normal;
    }
}
