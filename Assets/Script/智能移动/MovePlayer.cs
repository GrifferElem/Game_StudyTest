using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private InputSystem inputActions;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotSpeed = 5f;

    private void Awake()
    {
        inputActions = new InputSystem();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = inputActions.GamePlay.Move.ReadValue<Vector2>();
        Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movement = dir * moveSpeed * Time.deltaTime;

        transform.Translate(movement, Space.World);
        if (movement != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }
    }
}
