using UnityEngine;
using UnityEngine.Tilemaps;

public class Elevation : MonoBehaviour
{
    public TilemapCollider2D boundary;
    public BoxCollider2D[] door;
    public TilemapCollider2D ground;
    public TilemapCollider2D high;
    public TilemapCollider2D low;
    public BoxCollider2D[] stair;
    private bool lastState = true;

    BoxCollider2D FindNearestBoxCollider(BoxCollider2D[] allBoxColliders, Vector3 playerPosition)
    {
        BoxCollider2D nearestBoxCollider = null;
        float nearestDistance = float.MaxValue;
        foreach (BoxCollider2D boxCollider in allBoxColliders)
        {
            float distance = Vector3.Distance(playerPosition, boxCollider.bounds.center);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestBoxCollider = boxCollider;
            }
        }
        return nearestBoxCollider;
    }

    private void OnPlayerEnterMountain(bool isEnter)
    {
        if (lastState == isEnter)
            return;
        lastState = isEnter;
        high.enabled = isEnter;
        ground.enabled = !isEnter;
        low.enabled = !isEnter;
        foreach (BoxCollider2D obj in door)
        {
            obj.enabled = !isEnter;
        }
        boundary.enabled = isEnter;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;
        if (gameObject.tag != "Player")
            return;

        BoxCollider2D nearestBox = FindNearestBoxCollider(stair, gameObject.transform.position);
        float posYBox = nearestBox.bounds.center.y;
        bool isPlayerEnterMountain = gameObject.transform.position.y > posYBox;
        OnPlayerEnterMountain(isPlayerEnterMountain);
        gameObject.GetComponent<Player_Controller>()?.PlayerEnterMountain(isPlayerEnterMountain);
    }

    void Start()
    {
        OnPlayerEnterMountain(false);
    }
}
