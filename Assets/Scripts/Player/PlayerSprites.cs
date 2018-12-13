using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    private Transform playerSprite;
    private Transform weaponSprite;

    private CameraController cameraController;

    public void Init(Transform playerSprite, Transform weaponSprite, CameraController cameraController)
    {
        this.playerSprite = playerSprite;
        this.weaponSprite = weaponSprite;
        this.cameraController = cameraController;
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
