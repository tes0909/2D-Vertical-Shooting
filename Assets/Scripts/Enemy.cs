using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    [SerializeField] private int health;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float returnSpriteDelay = 0.1f; // 스프라이트 변경 후 지연 시간

    private readonly int _damagedSpriteIndex = 1; // 피해 입었을 때 사용할 스프라이트 인덱스
    private readonly int _defaultSpriteIndex; // 기본 인덱스
    private readonly string _borderBullet = "BorderBullet";
    private readonly string _playerBullet = "PlayerBullet";

    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnDamaged(int damage)
    {
        health -= damage;
        
        _spriteRenderer.sprite = sprites[_damagedSpriteIndex];
        StartCoroutine(ReturnSprite());
        
        if(health <= 0)
            Destroy(gameObject);
    }


    private IEnumerator ReturnSprite()
    {
        yield return new WaitForSeconds(returnSpriteDelay);
        _spriteRenderer.sprite = sprites[_defaultSpriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_borderBullet)) // 외벽
            Destroy(gameObject);
        else if (other.CompareTag(_playerBullet)) // 플레이어 총알
        {
            Bullet bullet = other.GetComponent<Bullet>();
            OnDamaged(bullet.damage);
            Destroy(other.gameObject); 
        }
    }
}
