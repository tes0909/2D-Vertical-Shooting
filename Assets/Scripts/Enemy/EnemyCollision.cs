using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private string _borderBullet = "BorderBullet";
    private string _playerBullet = "PlayerBullet";
    private EnemyHealth _enemyHealth;
    
    void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_borderBullet)) // 외벽
            Destroy(gameObject);
        else if (other.CompareTag(_playerBullet)) // 플레이어 총알
        {
            Bullet bullet = other.GetComponent<Bullet>();
            _enemyHealth.TakeDamaged(bullet.damage);
            Destroy(other.gameObject); 
        }
    }
}
