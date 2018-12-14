using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    private Transform playerSprite;
    private Transform weaponSprite;
    private Camera mainCam;

    public void Init(Transform playerSprite, Transform weaponSprite)
    {
        this.playerSprite = playerSprite;
        this.weaponSprite = weaponSprite;

        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        InputController.CursorInput += UpdateFacing;
    }

    private void OnDisable()
    {
        InputController.CursorInput -= UpdateFacing;
    }

    private void UpdateFacing(Vector2 cursorPosition)
    {
        UpdatePlayerFacing(cursorPosition);
        UpdateWeaponFacing();
    }

    private void UpdatePlayerFacing(Vector2 cursorPosition)
    {
        if (cursorPosition.x < playerSprite.position.x)
            playerSprite.localScale = new Vector3(-1, 1, 1);
        else
            playerSprite.localScale = new Vector3(1, 1, 1);
    }

    private void UpdateWeaponFacing()
    {
        //Point towards the mouse
        Vector3 pos = mainCam.WorldToScreenPoint(weaponSprite.position);
        Vector3 dir = Input.mousePosition - pos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponSprite.rotation = Quaternion.AngleAxis(weaponSprite.localScale.x > 0 ? angle : angle - 180f, Vector3.forward);
    }
}
