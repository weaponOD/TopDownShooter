using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    public void Init()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        InputController.AxisUp += OnAxisUp;
        InputController.AxisInput += OnAxisInput;
    }

    private void OnDisable()
    {
        InputController.AxisUp += OnAxisUp;
        InputController.AxisInput -= OnAxisInput;
    }

    private void OnAxisUp()
    {
        if (animator == null || animator.GetBool("moving") == false)
            return;

        animator.SetBool("moving", false);
    }

    private void OnAxisInput(Vector2 velocity)
    {
        if (animator == null || animator.GetBool("moving"))
            return;

        animator.SetBool("moving", true);
    }
}
