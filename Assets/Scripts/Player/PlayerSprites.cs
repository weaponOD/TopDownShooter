using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform weaponSprite;

    private SpriteRenderer[] playerSpriteRenderers;
    private CameraController cameraController;
    private InputController inputController;

    public void Init(CameraController cameraController, InputController inputController)
    {
        this.cameraController = cameraController;
        this.inputController = inputController;
       // playerSpriteRenderers = playerSprite.GetComponentsInChildren<SpriteRenderer>();
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
        {
            //playersprite.localpos.x *- playersprite.localpos.x
        }



        //for (int i = 0; i < playerSpriteRenderers.Length - 1; i++)
        //{
        //    if (cameraController.GetMousePosition().x < playerSprite.position.x)
        //        playerSpriteRenderers[i].flipX = true;
        //    else
        //        playerSpriteRenderers[i].flipX = false;
        //}
    }

    private void UpdateWeaponFacing()
    {
        if (cameraController == null)
            return;
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
