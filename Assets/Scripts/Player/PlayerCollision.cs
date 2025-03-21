using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private const string Enemy = "Enemy";
    private const string EnemyBullet = "EnemyBullet";
    public bool isDamaged;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Enemy) || other.gameObject.CompareTag(EnemyBullet))
        {
            if (isDamaged) return; // 중복 
            isDamaged = true;
            
            GameManager.Instance.RemoveLife();

            if (GameManager.Instance.PlayerLife == 0)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                GameManager.Instance.PlayerRespawn();
            }
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
