using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    private static InputHandler playerInput;

    private InputSystem input;
    private InputAction moveHorizontal;
    private InputAction moveVertical;
    private InputAction attack;
    private InputAction jump;
    private InputAction changeEquipment;
    private InputAction test;

    // get instance
    public static InputHandler GetInstance()
    {
        if (playerInput == null)
        {
            playerInput = new InputHandler();
        }
        return playerInput;
    }

    public InputHandler()
    {
        input = new InputSystem();
        input.Enable();
        input.Player.Enable();
        moveHorizontal = input.Player.MoveHorizontal;
        moveVertical = input.Player.MoveVertical;
        attack = input.Player.Attack;
        jump = input.Player.Jump;
        changeEquipment = input.Player.ChangeEquipment;
        test = input.Player.Test;
        SwitchWave(0);
    }

    public float GetHorizontal()
    {
        return moveHorizontal.ReadValue<Vector2>().x;
    }

    public float GetVertical()
    {
        return moveVertical.ReadValue<Vector2>().y;
    }

    public bool IsAttack()
    {
        return attack.triggered;
    }

    public bool IsJumpStarted()
    {
        return jump.IsInProgress();
    }

    public bool IsJumCanceled()
    {
        return jump.WasReleasedThisFrame();
    }

    public bool IsChangeEquipment()
    {
        return changeEquipment.triggered;
    }

    public bool IsTest()
    {
        return test.triggered;
    }

    public void SwitchWave(int wave)
    {
        if (wave == 0)
        {
            jump.Disable();
            moveVertical.Enable();
            return;
        }

        if (wave == 1)
        {
            jump.Enable();
            moveVertical.Disable();
        }
    }

    public void SetEnable(bool b)
    {
        if (b)
            input.Enable();
        else
            input.Disable();
    }
}
