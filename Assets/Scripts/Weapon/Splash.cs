using UnityEngine;

public class Splash : MonoBehaviour
{
    public float speed = 5;
    private Vector2 dir;
    private float lifeTime = 3f;
    private Rigidbody2D rb;

    public void Launch(Vector2 dir)
    {
        this.dir = dir;
        Refesh();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        gameObject.SetActive(false);
        collision
            .gameObject.GetComponent<Player_Controller>()
            ?.DealDame(GameConstants.ENERMY_TYPE.BOSS, transform, true);
    }

    public void Refesh()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = dir * speed;
        rb.simulated = true;
        Rotate();
    }

    public void Start()
    {
        Destroy(gameObject, lifeTime);
        Invoke("AddToPool", lifeTime);
    }

    private void AddToPool()
    {
        ObjectPoolManager.Instance.AddToPool(GameConstants.POOL_TYPE.ARROW, this.gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
