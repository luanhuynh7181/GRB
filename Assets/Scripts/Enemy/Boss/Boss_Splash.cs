using System.Collections;
using UnityEngine;

public class Boss_Splash : MonoBehaviour
{
    [SerializeField]
    private Transform launchPoint;

    private Rigidbody2D rb;

    private void Attack()
    {
        GameObject obj = ObjectPoolManager.Instance.GetFromPool(GameConstants.POOL_TYPE.SPLASH);
        obj.transform.position = launchPoint.position;
        Splash arrow = obj.GetComponent<Splash>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - launchPoint.position).normalized;
            arrow.Launch(direction);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
