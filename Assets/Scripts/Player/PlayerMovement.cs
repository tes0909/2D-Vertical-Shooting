using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
#if UNITY_STANDALONE || UNITY_EDITOR // PC: 키보드 방향 입력
        _movementDirection = direction;
        
#elif UNITY_ANDROID || UNITY_IOS
        // 모바일 입력: 손가락 드래그만큼 이동
        if (direction == Vector2.zero) return;

        // 손가락 위치로 바로 이동 (딜레이 없음)
        transform.position = Vector3.Lerp(transform.position, direction, speed * Time.deltaTime);
#endif
    }
    
    void ApplyMovement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR // PC에서만 velocity 적용
        if (player != null && player.Rb2d != null)
        {
            player.Rb2d.velocity = _movementDirection * speed;
        }
#endif
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }
}
