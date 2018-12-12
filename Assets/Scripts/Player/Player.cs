using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private Controller2D controller;
    private PlayerSprites sprites;
    private PlayerAnimation playerAnimation;

    public void Init(CameraController cameraController)
    {
        controller = GetComponent<Controller2D>();

        sprites = GetComponent<PlayerSprites>();
        sprites.Init(cameraController);

        playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.Init();
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
