using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int enemyscore;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float returnSpriteDelay = 0.1f; // 스프라이트 변경 후 지연 시간
    
    private int _damagedSpriteIndex = 1; // 피해 입었을 때 사용할 스프라이트 인덱스
    private int _defaultSpriteIndex; // 기본 인덱스
    private SpriteRenderer _spriteRenderer;
    private EnemyItemDrop _enemyItemDrop;
    private int _currentHealth;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyItemDrop = GetComponent<EnemyItemDrop>();
    }

    private void OnEnable()
    {
        _currentHealth = health;
        _spriteRenderer.sprite = sprites[_defaultSpriteIndex];
        transform.rotation = Quaternion.identity;
    }

    public void TakeDamaged(int damage)
    {
        if(_currentHealth <= 0) return;
        _currentHealth -= damage;

        _spriteRenderer.sprite = sprites[_damagedSpriteIndex];
        StartCoroutine(ReturnSprite());

        if (_currentHealth <= 0)
        {
            GameManager.Instance.AddScore(enemyscore);
            _enemyItemDrop.ItemDrop();
            GetComponent<ReturnObject>()?.ReturnObj();
            transform.rotation = Quaternion.identity;
        }
    }

    private IEnumerator ReturnSprite()
    {
        yield return new WaitForSeconds(returnSpriteDelay);
        _spriteRenderer.sprite = sprites[_defaultSpriteIndex];
    }
}
