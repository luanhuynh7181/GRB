using UnityEngine;

public class Player_Movement_Wave_0 : Player_Movement
{
    protected override void Init()
    {
        base.Init();
        GetComponent<Rigidbody2D>().gravityScale = 0;
        InputHandler.GetInstance().SwitchWave(0);
    }

    protected override void CheckInputMovement()
    {
        if (isKnock)
            return;
        float horizontal = InputHandler.GetInstance().GetHorizontal();
        float vertical = InputHandler.GetInstance().GetVertical();

        if (horizontal * transform.localScale.x < 0)
        {
            FlipPlayer();
        }
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}
