using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Enemy enemy;
    private const string BorderBullet = "BorderBullet";
    private const string PlayerBullet = "PlayerBullet";
    
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(BorderBullet)) // 외벽과 충돌
        {
            GetComponent<ReturnObject>()?.ReturnObj(); // => 몬스터 반환
            transform.rotation = Quaternion.identity;
        }
        else if (other.CompareTag(PlayerBullet)) // 플레이어 총알
        {
            Bullet bullet = other.GetComponent<Bullet>();
            enemy.EnemyHealth.TakeDamaged(bullet.damage);
            other.GetComponent<ReturnObject>()?.ReturnObj(); // => 총알 반환
        }
    }
}
