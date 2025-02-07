using System;
using System.Collections;
using System.Threading;
using GameConstants;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Map_Transition : MonoBehaviour
{
    [SerializeField]
    private float additivePos = 2f;

    [SerializeField]
    private CinemachineConfiner2D confiner;

    [SerializeField]
    private GameObject currentMap;

    [SerializeField]
    private Direction direction;

    [SerializeField]
    private MAP mapTo;

    [SerializeField]
    private GameObject nextMap;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    IEnumerator FixBugCamera()
    {
        yield return new WaitForSeconds(0.1f);
        confiner.gameObject.SetActive(true);
        currentMap.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        confiner.gameObject.SetActive(false);
        nextMap.SetActive(true);
        confiner.BoundingShape2D = nextMap.GetComponent<Collider2D>();
        UpdatePlayerPosition(collision.gameObject);
        confiner.InvalidateBoundingShapeCache();

        StartCoroutine(FixBugCamera());
        GlobalEventManager.TriggerOnChangeMap(mapTo);
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Vector3 newPos = player.transform.position;
        switch (direction)
        {
            case Direction.Up:
                newPos.y += additivePos;
                break;
            case Direction.Down:
                newPos.y -= additivePos;
                break;
            case Direction.Left:
                newPos.x -= additivePos;
                break;
            case Direction.Right:
                newPos.x += additivePos;
                break;
        }
        player.transform.position = newPos;
    }
}
