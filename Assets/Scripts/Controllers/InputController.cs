using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum KeyInputType { Down, Held, Up }

public class InputController : MonoBehaviour
{   
    public Action<Vector2> axisInput;
    public Action<KeyInputType> shootInput;
    public Action<KeyInputType> dodgeInput;

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
            return;

        axisInput.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void CheckShootInput()
    {
        if (Input.GetKeyDown(gameSettings.shootKey))
            shootInput.Invoke(KeyInputType.Down);
        else if (Input.GetKey(gameSettings.shootKey))
            shootInput.Invoke(KeyInputType.Held);
        else if (Input.GetKeyUp(gameSettings.shootKey))
            shootInput.Invoke(KeyInputType.Up);
    }

    private void CheckDodgeInput()
    {
        if (Input.GetKeyDown(gameSettings.dodgeKey))
            dodgeInput.Invoke(KeyInputType.Down);
        else if (Input.GetKey(gameSettings.dodgeKey))
            dodgeInput.Invoke(KeyInputType.Held);
        else if (Input.GetKeyUp(gameSettings.dodgeKey))
            dodgeInput.Invoke(KeyInputType.Up);
    }
}
