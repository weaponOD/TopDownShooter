using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private int horizontalRayCount = 4;
    [SerializeField] private int verticalRayCount = 4;
    [SerializeField] private LayerMask collisionMask;

    [Header("Weapon")]
    [SerializeField] private int startingWeaponId;
    [SerializeField] private LayerMask damageLayer;

    [Header("Sprites")]
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform weaponSprite;

    private Controller2D controller;
    private PlayerSprites sprites;
    private PlayerAnimation playerAnimation;
    private PlayerWeapon playerWeapon;

    public void Init(CameraController cameraController)
    {
        controller = GetComponent<Controller2D>();
        controller.Init(horizontalRayCount, verticalRayCount, collisionMask);

        sprites = GetComponent<PlayerSprites>();
        sprites.Init(playerSprite, weaponSprite, cameraController);

        playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.Init();

        playerWeapon = GetComponent<PlayerWeapon>();
        playerWeapon.Init(weaponSprite, startingWeaponId, damageLayer);
    }

    private void OnEnable()
    {
        InputController.AxisInput += UpdateVelocity;
    }

    private void OnDisable()
    {
        InputController.AxisInput -= UpdateVelocity;
    }

    private void UpdateVelocity(Vector2 velocity)
    {
        velocity = Vector2.ClampMagnitude(velocity, 1);
        velocity *= moveSpeed * Time.deltaTime;

        controller.Move(velocity);
    }
}
