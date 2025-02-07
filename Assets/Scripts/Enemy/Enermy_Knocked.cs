using System.Collections;
using GameConstants;
using UnityEngine;

public class Enermy_Knocked : MonoBehaviour
{
    public void KnockBack(Transform obj, float force, float knockTime)
    {
        Vector2 dir = (transform.position - obj.position).normalized * force;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir;
        GetComponent<Enermy_Movement>()?.ChangeState(CHARACTER_STATE.KNOCK);
        StartCoroutine(StunTimer(knockTime));
    }

    IEnumerator StunTimer(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Enermy_Movement>().ChangeState(CHARACTER_STATE.IDLE);
    }
}
