using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpAndMove : MonoBehaviour
{
    private Vector3 dir;
    private Rigidbody rb;

    public InputSystem inputActions;
    public float moveSpeed = 5f;
    public float jumpSpeed = 3f;

    private void Awake()
    {
        inputActions = new InputSystem();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.GamePlay.Jump.performed += Jump;
    }
    private void OnDisable()
    {
        inputActions.GamePlay.Jump.performed -= Jump;

        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = inputActions.GamePlay.Move.ReadValue<Vector2>();
        dir = new Vector3(moveInput.x, 0, moveInput.y);
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        RotateToMoveDir();
    }
    private void RotateToMoveDir()
    {
        if (dir.sqrMagnitude > 0.01f)
        {
            transform.forward = dir;
        }
    }
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
    }
}
