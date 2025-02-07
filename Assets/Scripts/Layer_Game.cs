using UnityEngine;

public static class LayerUtils
{
    public static LayerMask? GetEnermyLayerByPlayer(GameObject gameObject)
    {
        int layerIndex = gameObject.layer;
        if (layerIndex == ((int)Layer_Collision.Player_Ground))
        {
            return (int)Layer_Collision.Enermy_Ground;
        }
        if (layerIndex == ((int)Layer_Collision.Player_Mountain))
        {
            return (int)Layer_Collision.Enermy_Mountain;
        }
        return null;
    }

    public static LayerMask? GetPlayerLayerByEnermy(GameObject gameObject)
    {
        int layerIndex = gameObject.layer;
        if (layerIndex == ((int)Layer_Collision.Enermy_Ground))
        {
            return (int)Layer_Collision.Player_Ground;
        }

        if (layerIndex == ((int)Layer_Collision.Enermy_Mountain))
        {
            return (int)Layer_Collision.Player_Mountain;
        }
        return null;
    }
}

public enum Layer_Collision
{
    Default = 0,
    TransparentFX = 1,
    IgnoreRaycast = 2,

    // Layer 3 is unused in your example
    Water = 4,
    UI = 5,
    Player_Ground = 6,
    Player_Mountain = 7,
    Enermy_Ground = 8,
    Enermy_Mountain = 9,
    Obstacle = 10,
    Enemy_Weapon_Mountain = 11,
    Enemy_Weapon_Ground = 12,
    Player_Weapon_Mountain = 13,
    Player_Weapon_Ground = 14,
}

public enum Layer_Sorting
{
    Ground,
    Mountain,
}
