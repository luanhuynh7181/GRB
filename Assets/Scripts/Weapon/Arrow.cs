using GameConstants;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public OnArrowCollision callback = null;

    [SerializeField]
    private Sprite buriedSprite;

    private Vector2 dir = Vector2.zero;
    private float lifeTime = 3f;

    [SerializeField]
    private Sprite normalSprite;

    private Rigidbody2D rb;
    private float defaultSpeed = 15f;
    private float speed = 15f;

    public delegate void OnArrowCollision(Collision2D collision);

    public void AttachToTarget(Transform target)
    {
        GetComponent<SpriteRenderer>().sprite = buriedSprite;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;

        transform.SetParent(target, true);
    }

    public void Init(
        Vector2 _dir,
        OnArrowCollision _cb,
        LayerMask layer,
        SpriteRenderer root,
        float ratioSpeed = 1
    )
    {
        callback = _cb;
        dir = _dir;
        gameObject.layer = layer;
        speed = defaultSpeed * ratioSpeed;
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingLayerID = root.sortingLayerID;
        sprite.sortingOrder = root.sortingOrder;
        sprite.sprite = normalSprite;
        Invoke("AddToPool", lifeTime);
        Refesh();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        AttachToTarget(collision.transform);
        callback?.Invoke(collision);
    }

    public void Refesh()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = dir * speed;
        transform.localScale = Vector3.one;
        rb.simulated = true;
        RotateArrow();
    }

    private void AddToPool()
    {
        ObjectPoolManager.Instance.AddToPool(POOL_TYPE.ARROW, this.gameObject);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
