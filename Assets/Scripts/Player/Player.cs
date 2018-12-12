using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private Controller2D controller;
    private PlayerSprites sprites;

    public void Init(CameraController cameraController)
    {
        controller = GetComponent<Controller2D>();

        sprites = GetComponent<PlayerSprites>();
        sprites.Init(cameraController);
    }

    private void OnEnable()
    {
        InputController.axisInput += UpdateVelocity;
    }

    private void OnDisable()
    {
        InputController.axisInput -= UpdateVelocity;
    }

    private void UpdateVelocity(Vector2 velocity)
    {
        velocity = Vector2.ClampMagnitude(velocity, 1);
        velocity *= moveSpeed * Time.deltaTime;

        controller.Move(velocity);
    }
}
