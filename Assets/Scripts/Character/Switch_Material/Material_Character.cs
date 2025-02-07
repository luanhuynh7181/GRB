using UnityEngine;

public class Material_Character : MonoBehaviour
{
    public Material material { get; set; }
    public float timeRunEffect { get; set; }

    public virtual void RunEffect() { }
}
