using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Gun : MonoBehaviour
{
    [SerializeField]
    protected GameObject cluster;

    [SerializeField]
    protected Laser_Collision collisionScript;

    [SerializeField]
    protected GameObject dirLine;

    protected float groundY = 5.2f;
    protected bool isActived = false;

    [SerializeField]
    protected LineRenderer lineRenderer;

    [SerializeField]
    protected GameObject particle;

    protected STATE state;
    protected float timeActive = 2;
    protected float timeDelay = 2;
    protected float timer = 0;

    protected enum STATE
    {
        OutRange,
        Active,
        CoolDown,
    }

    public virtual void Init() { }

    public virtual void OnTriggerPlayer(GameObject player) { }

    protected virtual void ChangeState(STATE newState)
    {
        state = newState;
        timer = 0;
        switch (newState)
        {
            case STATE.Active:
            {
                lineRenderer.gameObject.SetActive(true);
                collisionScript.SetEnableCollision(true);
                particle.SetActive(true);
                lineRenderer.material.SetFloat("_isThickness", 0);
                var endPoint = lineRenderer.GetPosition(1);
                particle.transform.position = endPoint;
                break;
            }
            case STATE.OutRange:
            {
                lineRenderer.gameObject.SetActive(false);
                collisionScript.SetEnableCollision(false);
                particle.SetActive(false);
                break;
            }
            case STATE.CoolDown:
            {
                lineRenderer.gameObject.SetActive(true);
                lineRenderer.material.SetFloat("_isThickness", 1);
                collisionScript.SetEnableCollision(false);
                particle.SetActive(true);
                Transform obj = particle.transform;
                obj.localPosition = new Vector3(0, 1, 0);
                obj.localScale = new Vector3(0, 0, 0);
                LeanTween.scale(obj.gameObject, new Vector3(1, 1, 1), timeDelay).setDelay(0.5f);
                break;
            }
        }
    }

    protected void UpdateLiner(Vector3 director)
    {
        Vector3 startPoint = cluster.transform.position;
        Vector3 dir = (director - startPoint).normalized;

        float deltaY = startPoint.y - groundY;
        float hypotenuse = Mathf.Abs(deltaY / dir.y);
        hypotenuse = Mathf.Clamp(hypotenuse, 0, EnermiesStat.Ins.laserMaxWidth);
        Vector3 endPoint = hypotenuse * dir + startPoint;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        collisionScript.UpdateCollider(hypotenuse);
        if (state == STATE.Active)
        {
            particle.transform.position = endPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        OnTriggerPlayer(collision.gameObject);
        ChangeState(STATE.CoolDown);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        ChangeState(STATE.OutRange);
    }

    void Start()
    {
        ChangeState(STATE.OutRange);
        timeActive = EnermiesStat.Ins.timeActive;
        timeDelay = EnermiesStat.Ins.timeRest;
        Init();
    }

    void Update()
    {
        if (state != STATE.OutRange)
        {
            timer += Time.deltaTime;
            if (state == STATE.Active)
            {
                if (timer >= timeActive)
                {
                    ChangeState(STATE.CoolDown);
                }
            }
            else if (state == STATE.CoolDown)
            {
                if (timer >= timeDelay)
                {
                    ChangeState(STATE.Active);
                }
            }
        }
    }
}
