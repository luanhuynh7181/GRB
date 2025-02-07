using System.Runtime.CompilerServices;

using UnityEngine;

public class Laser_Collision : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    public void UpdateCollider(float width)
    {
        boxCollider2D.offset = new Vector2(0, -width / 2);
        boxCollider2D.size = new Vector2(0.8f, width);

        circleCollider2D.offset = new Vector2(0, -width);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Controller>()
                 ?.DealDame(GameConstants.ENERMY_TYPE.LASER_GUN, transform);
        }
    }

    public void SetEnableCollision(bool isEnable)
    {
        boxCollider2D.enabled = isEnable;
        circleCollider2D.enabled = isEnable;
    }
}

