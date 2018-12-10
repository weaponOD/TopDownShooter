using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 6f;

    private Controller2D controller;

    private Vector2 velocity;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
