using System;
using UnityEngine;

public class Camera_Parallax : MonoBehaviour
{
    private float lastX;

    [SerializeField]
    private GameObject mainCamera;

    // add event dispatcher
    void Awake()
    {
        lastX = mainCamera.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = mainCamera.transform.position.x;
        float deltaX = currentX - lastX;
        lastX = currentX;
        if (deltaX != 0)
        {
            GlobalEventManager.TriggerOnCameraMove(deltaX);
        }
    }
}
