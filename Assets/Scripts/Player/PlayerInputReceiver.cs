using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerInputReceiver : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnShootEvent;
    public event Action OnBoomEvent;
    private Camera _mainCamera;
    
    void Awake()
    {
        _mainCamera = Camera.main;
        EnhancedTouchSupport.Enable();
    }
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];

            // 손가락 위치 → 월드 좌표 변환
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(touch.screenPosition);
            worldPos.z = 0;

            OnMoveEvent?.Invoke(worldPos);
        }
        else
        {
            // 손가락 뗐을 때 멈춤
            OnMoveEvent?.Invoke(Vector2.zero);
        }
#endif
    }
    
    public void OnMove(InputValue value)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        Vector2 input = value.Get<Vector2>();
        OnMoveEvent?.Invoke(input);
#endif
    }

    public void OnBoom(InputValue value)
    {
        OnBoomEvent?.Invoke();
    }
}
