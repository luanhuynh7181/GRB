using System;
using GameConstants;
using UnityEngine;

public class GlobalEventManager
{
    public static event Action<bool> ShowPopupEndGame;

    public static event Action<float> OnCameraMove;
    public static event Action<MAP> OnChangeMap;
    public static event Action<float> OnMushroomEaten;
    public static event Action<Save_Point> SavePoint;
    public static event Action<MAP> NotiSaveMap;
    public static event Action<GameObject> OnEnermyDead;

    public static void TriggerOnCameraMove(float deltaX)
    {
        OnCameraMove?.Invoke(deltaX);
    }

    public static void TriggerOnChangeMap(MAP mapTo)
    {
        OnChangeMap?.Invoke(mapTo);
    }

    public static void TriggerOnMushroomEaten(float HPHeal)
    {
        OnMushroomEaten?.Invoke(HPHeal);
    }

    public static void TriggerShowPopupEndGame(bool b)
    {
        ShowPopupEndGame?.Invoke(b);
    }

    public static void TriggerSavePoint(Save_Point point)
    {
        SavePoint?.Invoke(point);
    }

    public static void TriggerNotiSaveMap(MAP index)
    {
        NotiSaveMap?.Invoke(index);
    }

    public static void TriggerOnEnermyDead(GameObject enermy)
    {
        OnEnermyDead?.Invoke(enermy);
    }
}
