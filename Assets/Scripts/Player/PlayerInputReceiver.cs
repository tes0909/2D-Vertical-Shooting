using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnShootEvent;
    private Camera _mainCamera;
    
    void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        OnMoveEvent?.Invoke(input);
    }

    public void OnShoot(InputValue value)
    {
        OnShootEvent?.Invoke();
    }
}
