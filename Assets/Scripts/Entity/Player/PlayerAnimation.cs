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
        //InputController.ShootInput += OnShootInput;
    }

    private void OnDisable()
    {
        InputController.AxisUp += OnAxisUp;
        InputController.AxisInput -= OnAxisInput;
        //InputController.ShootInput -= OnShootInput;

    }

    /// <summary>
    /// Plays shoot animation when firing projectile
    /// </summary>
    //private void OnShootInput()
    //{
    //    if (animator == null || animator.GetBool("moving") == false)
    //          return;
    //
    //    animator.SetTrigger("shoot");
    //}


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
