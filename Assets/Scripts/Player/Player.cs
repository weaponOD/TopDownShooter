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
    private PlayerSprites sprites;
    private Animator playerAnimator;

    private Vector2 velocity;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
        cameraController = Camera.main.GetComponent<CameraController>();
        sprites = GetComponent<PlayerSprites>();
        sprites.Init(cameraController);
    }

    private void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = Vector2.ClampMagnitude(velocity, 1);
        controller.Move(velocity * moveSpeed * Time.deltaTime);

        bool result = velocity > 0 ? true : false;
    }

    private bool HandleAnims(float v)
    {
       bool result = v > 0 ?  true :  false;

    }
}
