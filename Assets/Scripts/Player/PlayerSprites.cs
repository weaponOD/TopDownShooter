using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform weaponSprite;

    private CameraController cameraController;
    private InputController inputController;

    public void Init(CameraController cameraController, InputController inputController)
    {
        this.cameraController = cameraController;
        this.inputController = inputController;
    }

    void Update()
    {
        UpdatePlayerFacing();
        UpdateWeaponFacing();
    }

    private void UpdatePlayerFacing()
    {
        if (cameraController == null)
            return;

        if (cameraController.GetMousePosition().x < playerSprite.position.x)
            playerSprite.localScale = new Vector3(-1, 1, 1);
        else
            playerSprite.localScale = new Vector3(1, 1, 1);
    }

    private void UpdateWeaponFacing()
    {
        if (cameraController == null)
            return;

        //Point towards the mouse
        Vector3 pos = cameraController.GetScreenPoint(weaponSprite.position);
        Vector3 dir = Input.mousePosition - pos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponSprite.rotation = Quaternion.AngleAxis(weaponSprite.localScale.x > 0 ? angle : angle - 180f, Vector3.forward);
    }
}
