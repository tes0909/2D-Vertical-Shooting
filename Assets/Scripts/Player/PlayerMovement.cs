using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private PlayerInputReceiver _playerInputReceiver;
    [SerializeField] private float speed = 3.0f;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();
    }

    private void OnEnable()
    {
        _playerInputReceiver.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
    void ApplyMovement()
    {
        _rb2d.velocity = _movementDirection * speed;
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }
}
