using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSprites))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private Controller2D controller;
    private CameraController cameraController;
    private InputController inputController;
    private PlayerSprites sprites;
    private Animator playerAnimator;

    public void Init(CameraController cameraController, InputController inputController)
    {
    //    playerAnimator = GetComponent<Animator>();
    //    controller = GetComponent<Controller2D>();
    //    this.cameraController = cameraController;
    //    this.inputController = inputController;
    //    inputController.axisInput += UpdateVelocity;

    //    sprites = GetComponent<PlayerSprites>();
    //    sprites.Init(cameraController, inputController);
    //}

    //private void OnDisable()
    //{
    //    inputController.axisInput -= UpdateVelocity;
    //}

    //private void UpdateVelocity(Vector2 velocity)
    //{
    //    velocity = Vector2.ClampMagnitude(velocity, 1);

    //    controller.Move(velocity * moveSpeed * Time.deltaTime);

    //    bool result = velocity > 0 ? true : false;
    //}

    //private bool HandleAnims(float v)
    //{
    //    bool result = v > 0 ? true : false;

    //    v *= moveSpeed * Time.deltaTime;

    //    controller.Move(ref velocity);

    //}
}
