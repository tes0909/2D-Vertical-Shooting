using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private const string Enemy = "Enemy";
    private const string EnemyBullet = "EnemyBullet";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Enemy) || other.gameObject.CompareTag(EnemyBullet))
        {
            GameManager.Instance.PlayerRespawn();
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
