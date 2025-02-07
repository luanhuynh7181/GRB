using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround_Parallax : MonoBehaviour
{
    private RawImage image;
    private float pos = 0;

    [SerializeField]
    private float speed = 0;

    void Awake()
    {
        image = GetComponent<RawImage>();
    }

    void Move(float dir)
    {
        pos += dir * speed * Time.deltaTime;
        if (pos > 1.0F)
            pos -= 1.0F;
        image.uvRect = new Rect(pos, 0, 1, 1);
    }

    private void OnDisable()
    {
        GlobalEventManager.OnCameraMove -= Move;
    }

    // add OnplayerMove on Enable and remove on Disable
    private void OnEnable()
    {
        GlobalEventManager.OnCameraMove += Move;
    }
}
