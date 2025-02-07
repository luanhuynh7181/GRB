using UnityEngine;

public static class Utility
{
    public static void ChangeLayerSorting(GameObject _object, bool isGround)
    {
        _object.GetComponent<SpriteRenderer>().sortingLayerName = isGround
            ? Layer_Sorting.Ground.ToString()
            : Layer_Sorting.Mountain.ToString();
    }
}
