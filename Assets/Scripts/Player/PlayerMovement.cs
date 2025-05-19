using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float speed = 3.0f;
    private Vector2 _movementDirection;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveEvent());
    }

    IEnumerator MoveEvent()
    {
        while (player == null || player.PlayerInputReceiver == null)
            yield return null;
        player.PlayerInputReceiver.OnMoveEvent += Move;    
    }

    private void OnDisable()
    {
        player.PlayerInputReceiver.OnMoveEvent -= Move;
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
    void ApplyMovement()
    {
        player.Rb2d.velocity = _movementDirection * speed;
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }
}
