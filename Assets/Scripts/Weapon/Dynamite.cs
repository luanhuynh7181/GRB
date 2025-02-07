using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private float height;
    private Vector2 target;

    public void ExploseEnd() { }

    public void Init(Vector2 _target, float height)
    {
        target = _target;
        this.height = height;
        Refresh();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;
        collision
            .gameObject.GetComponent<Player_Controller>()
            ?.DealDame(GameConstants.ENERMY_TYPE.GOBLIN, transform, true);
    }

    IEnumerator MoveParabola()
    {
        float time = 0;
        Vector2 start = transform.position;

        while (time < 1)
        {
            time += Time.deltaTime;

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
        GetComponent<Animator>().Play("Idle");
        StartCoroutine(MoveParabola());
    }

    private void AddToPool()
    {
        ObjectPoolManager.Instance.AddToPool(GameConstants.POOL_TYPE.DYNAMITE, gameObject);
    }
}
