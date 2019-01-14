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
    [SerializeField] private float dodgeSpeed = 20f;
    [SerializeField] private float dodgeTime = 1f;
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

    private bool dodged;
    private bool startedDodge;

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
        InputController.DodgeInput += DodgeInput;
    }

    private void OnDisable()
    {
        InputController.AxisInput -= UpdateVelocity;
        InputController.DodgeInput -= DodgeInput;
    }

    private void UpdateVelocity(Vector2 velocity)
    {
        velocity = Vector2.ClampMagnitude(velocity, 1);

        if (dodged == false && startedDodge == false)
        {
            velocity *= moveSpeed * Time.deltaTime;
            controller.Move(velocity);
        }
        else if (startedDodge == false)
            StartCoroutine(Dodge(velocity));
    }

    private void DodgeInput(KeyInputType inputType)
    {
        if (inputType != KeyInputType.Down)
            return;

        dodged = true;
        this.Invoke(ResetDodge, 0.05f);
    }

    private IEnumerator Dodge(Vector2 velocity)
    {
        startedDodge = true;
        float t = 0f;

        Debug.Log("Yes");

        while(t < dodgeTime)
        {
            t += Time.deltaTime;
            controller.Move(velocity * dodgeSpeed * Time.deltaTime);
            yield return null;
        }


        startedDodge = false;
    }

    private void ResetDodge()
    {
        dodged = false;
    }
}
