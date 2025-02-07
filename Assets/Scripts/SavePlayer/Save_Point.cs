using GameConstants;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Save_Point : MonoBehaviour
{
    public GameObject[] prerequisite;
    public Vector2 offsetPoint;
    private Animator animator;
    public MAP map;
    private bool isSaved = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GlobalEventManager.OnEnermyDead += OnEnermyDead;
    }

    private void OnDisable()
    {
        GlobalEventManager.OnEnermyDead -= OnEnermyDead;
    }

    private void OnEnermyDead(GameObject enermy)
    {
        if (isSaved)
            return;
        if (!CanSave(enermy))
            return;
        Active();
    }

    private bool CanSave(GameObject except)
    {
        if (map == MAP.MAP_1)
        {
            return true;
        }
        foreach (var obj in prerequisite)
        {
            if (obj == null)
                continue;
            if (obj == except)
                continue;
            if (obj.gameObject.activeInHierarchy)
                return false;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Player"))
            return;

        if (isSaved)
            return;
        if (!CanSave(null))
            return;
        GlobalEventManager.TriggerSavePoint(this);
        GetComponent<Collider2D>().enabled = false;

        isSaved = true;
        animator.SetTrigger("Show");
    }

    public void InActive()
    {
        animator.SetBool("IsActive", false);
        if (CanSave(null))
            Active();
    }

    public void Active()
    {
        animator.SetBool("IsActive", true);
    }

    public Vector2 GetPointRespawn()
    {
        return (Vector2)transform.position + offsetPoint;
    }

    public void Passed()
    {
        gameObject.SetActive(false);
        foreach (var obj in prerequisite)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((Vector2)transform.position + offsetPoint, 0.1f);
    }
}
