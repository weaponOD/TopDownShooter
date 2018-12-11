using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform weaponSprite;

    private SpriteRenderer playerSpriteRenderer;
    private CameraController cameraController;

    public void Init(CameraController cameraController)
    {
        this.cameraController = cameraController;
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdatePlayerFacing();
        UpdateWeaponFacing();
    }

    private void UpdatePlayerFacing()
    {
        if (cameraController.GetMousePosition().x < playerSprite.position.x)
            playerSpriteRenderer.flipX = true;
        else
            playerSpriteRenderer.flipX = false;

    }

    private void UpdateWeaponFacing()
    {
        //if (weaponSprite)
        //{
        //    if (cameraController.GetMousePosition().x < weaponSprite.position.x)
        //        weaponSprite.localScale = new Vector3(-1, 1, 1);
        //    else
        //        weaponSprite.localScale = new Vector3(1, 1, 1);
        //}

        //Point towards the mouse
        Vector3 pos = cameraController.GetScreenPoint(weaponSprite.position);
        Vector3 dir = Input.mousePosition - pos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponSprite.rotation = Quaternion.AngleAxis(weaponSprite.localScale.x > 0 ? angle : angle - 180f, Vector3.forward);
    }
}
