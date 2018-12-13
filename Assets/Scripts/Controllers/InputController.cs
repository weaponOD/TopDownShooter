using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KeyInputType { Down, Held, Up }

public class InputController : MonoBehaviour
{
    public static event Action<Vector2> AxisInput = delegate { };
    public static event Action AxisUp = delegate { };
    public static event Action<KeyInputType> ShootInput = delegate { };
    public static event Action<KeyInputType> DodgeInput = delegate { };

    private GameSettings gameSettings;

    public void Init(GameSettings gameSettings)
    {
        this.gameSettings = gameSettings;
    }

    private void Update()
    {
        if (gameSettings == null)
            return;

        CheckAxisInput();
        CheckShootInput();
        CheckDodgeInput();
    }

    private void CheckAxisInput()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            AxisUp.Invoke();
            return;
        }

        AxisInput.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void CheckShootInput()
    {
        if (Input.GetMouseButtonDown(0))
            ShootInput.Invoke(KeyInputType.Down);
        else if (Input.GetMouseButton(0))
            ShootInput.Invoke(KeyInputType.Held);
        else if (Input.GetMouseButtonUp(0))
            ShootInput.Invoke(KeyInputType.Up);
    }

    private void CheckDodgeInput()
    {
        if (Input.GetMouseButtonDown(1))
            DodgeInput.Invoke(KeyInputType.Down);
        else if (Input.GetMouseButton(1))
            DodgeInput.Invoke(KeyInputType.Held);
        else if (Input.GetMouseButtonUp(1))
            DodgeInput.Invoke(KeyInputType.Up);
    }
}
