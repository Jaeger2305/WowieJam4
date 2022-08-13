using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    private Rigidbody2D _rb;
    private PlayerGFX _playerGFX;
    private Vector2 moveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerGFX = GetComponent<PlayerGFX>();
    }

    void FixedUpdate()
    {
        float speed = (moveInput * _moveSpeed).magnitude;
        _rb.MovePosition(_rb.position + moveInput * _moveSpeed * Time.fixedDeltaTime);
        _playerGFX.SetWalking(speed);
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
