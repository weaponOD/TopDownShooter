using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(Controller2D))]
public class Player : Entity
{
    [Header("Player Settings")]
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private int horizontalRayCount = 4;
    [SerializeField] private int verticalRayCount = 4;
    [SerializeField] private LayerMask collisionMask;

    [Header("Weapon")]
    [SerializeField] private int startingWeaponId;
    [SerializeField] private LayerMask damageLayer;

    [Header("Sprites")]
    [SerializeField] private Transform weaponSprite;

    [Header("UI")]
    [SerializeField] private string heartTag;
    [SerializeField] private string ammoTag;

    private Controller2D controller;
    private PlayerSprites playerSprites;
    private PlayerAnimation playerAnimation;
    private PlayerWeapon playerWeapon;

    private HealthUI healthUI;
    private AmmoUI ammoUI;

    public override void Init()
    {
        base.Init();

        controller = GetComponent<Controller2D>();
        controller.Init(horizontalRayCount, verticalRayCount, collisionMask);

        playerSprites = GetComponent<PlayerSprites>();
        playerSprites.Init(sprites, weaponSprite);

        playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.Init();

        playerWeapon = GetComponent<PlayerWeapon>();
        playerWeapon.Init(weaponSprite, startingWeaponId, damageLayer, GameObject.FindWithTag(ammoTag).GetComponent<AmmoUI>());

        healthUI = GameObject.FindWithTag(heartTag).GetComponent<HealthUI>();
        healthUI.Init(healthSettings.maxHealth);
    }

    public override void Damaged(int amount)
    {
        base.Damaged(amount);
        healthUI.UpdateHearts(health.CurrentHealth);
    }

    public override void Healed(int amount)
    {
        base.Healed(amount);
        healthUI.UpdateHearts(health.CurrentHealth);
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
