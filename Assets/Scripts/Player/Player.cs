﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 6f;

    private Controller2D controller;
    private CameraController cameraController;
    private InputController inputController;
    private PlayerSprites sprites;

    public void Init(CameraController cameraController, InputController inputController)
    {
        controller = GetComponent<Controller2D>();
        this.cameraController = cameraController;
        this.inputController = inputController;
        inputController.axisInput += UpdateVelocity;

        sprites = GetComponent<PlayerSprites>();
        sprites.Init(cameraController, inputController);
    }

    private void OnDisable()
    {
        inputController.axisInput -= UpdateVelocity;
    }

    private void UpdateVelocity(Vector2 velocity)
    {
        velocity = Vector2.ClampMagnitude(velocity, 1);
        velocity *= moveSpeed * Time.deltaTime;

        controller.Move(ref velocity);
    }
}
