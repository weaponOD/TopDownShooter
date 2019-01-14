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
    public static event Action<KeyInputType> ReloadInput = delegate { };
    public static event Action<KeyInputType> DodgeInput = delegate { };
    public static event Action<Vector2> CursorInput = delegate { };

    private GameSettings gameSettings;
    private CameraController cameraController;
    private Vector3 previousCursorPosition;

    public void Init(GameSettings gameSettings, CameraController cameraController)
    {
        this.gameSettings = gameSettings;
        this.cameraController = cameraController;
    }

    private void Update()
    {
        if (gameSettings == null)
            return;

        CheckAxisInput();
        CheckShootInput();
        CheckDodgeInput();
        CheckReloadInput();
        CheckCursorInput();
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
        if (Input.GetKeyDown(gameSettings.shootKey))
            ShootInput.Invoke(KeyInputType.Down);
        else if (Input.GetKey(gameSettings.shootKey))
            ShootInput.Invoke(KeyInputType.Held);
        else if (Input.GetKeyUp(gameSettings.shootKey))
            ShootInput.Invoke(KeyInputType.Up);
    }

    private void CheckDodgeInput()
    {
        if (Input.GetKeyDown(gameSettings.dodgeKey))
            DodgeInput.Invoke(KeyInputType.Down);
        else if (Input.GetKey(gameSettings.dodgeKey))
            DodgeInput.Invoke(KeyInputType.Held);
        else if (Input.GetKeyUp(gameSettings.dodgeKey))
            DodgeInput.Invoke(KeyInputType.Up);
    }

    private void CheckReloadInput()
    {
        if (Input.GetKeyDown(gameSettings.reloadKey))
            ReloadInput.Invoke(KeyInputType.Down);
        else if (Input.GetKey(gameSettings.reloadKey))
            ReloadInput.Invoke(KeyInputType.Held);
        else if (Input.GetKeyUp(gameSettings.reloadKey))
            ReloadInput.Invoke(KeyInputType.Up);
    }

    private void CheckCursorInput()
    {
        if(previousCursorPosition != cameraController.GetMousePosition())
        {
            previousCursorPosition = cameraController.GetMousePosition();
            CursorInput.Invoke(previousCursorPosition);
        }
    }
}
