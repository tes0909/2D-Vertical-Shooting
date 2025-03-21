using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int life = 1;
    private const string Enemy = "Enemy";
    private const string EnemyBullet = "EnemyBullet";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Enemy) || other.gameObject.CompareTag(EnemyBullet))
        {
            GameManager.Instance.RemoveLife(life);

            if (life == 0)
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
