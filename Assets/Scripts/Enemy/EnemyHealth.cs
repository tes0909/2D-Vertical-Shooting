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
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamaged(int damage)
    {
        health -= damage;
        
        _spriteRenderer.sprite = sprites[_damagedSpriteIndex];
        StartCoroutine(ReturnSprite());

        if (health <= 0)
        {
            GameManager.Instance.PlayerScore += enemyscore;
            Destroy(gameObject);
        }
            
    }

    private IEnumerator ReturnSprite()
    {
        yield return new WaitForSeconds(returnSpriteDelay);
        _spriteRenderer.sprite = sprites[_defaultSpriteIndex];
    }
}
