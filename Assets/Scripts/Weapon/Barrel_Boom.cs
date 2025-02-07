using System.Collections;
using GameConstants;
using UnityEngine;

public class Barrel_Boom : MonoBehaviour
{
    private float height;
    private bool isExplose = false;
    private float speed;
    private Vector2 target;

    public void Explose()
    {
        isExplose = true;
    }

    public void ExploseEnd()
    {
        ObjectPoolManager.Instance.AddToPool(GameConstants.POOL_TYPE.BARREL_BOOM, gameObject);
    }

    public void Init(Vector2 _target, float height, float speed)
    {
        target = _target;
        this.height = height;
        this.speed = speed;
        Refresh();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        if (!isExplose)
            return;
        isExplose = false;
        collision
            .gameObject.GetComponent<Player_Controller>()
            ?.DealDame(ENERMY_TYPE.BARREL_BOOM, transform, false);
    }

    IEnumerator MoveParabola()
    {
        float time = 0;
        Vector2 start = transform.position;

        while (time < 1)
        {
            time += Time.deltaTime * speed;

            float x = Mathf.Lerp(start.x, target.x, time);
            float y = Mathf.Lerp(start.y, target.y, time) + height * Mathf.Sin(Mathf.PI * time);
            transform.position = new Vector2(x, y);

            yield return null;
        }
        transform.position = target;
        GetComponent<Animator>().Play("Idle");
    }

    private void Refresh()
    {
        isExplose = false;
        GetComponent<Animator>().Play("Stop");
        StartCoroutine(MoveParabola());
    }
}
