using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private const string Enemy = "Enemy";
    private const string EnemyBullet = "EnemyBullet";
    private const string Items = "Item";
    public bool isDamaged;

    [SerializeField] private GameObject boomEffect;
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
        else if (other.gameObject.CompareTag(Items))
        {
            Item item = other.gameObject.GetComponent<Item>();
            PlayerShooting playerShooting = GetComponent<PlayerShooting>();
            switch (item.GetItemType())
            {
                case Item.ItemType.Coin:
                    GameManager.Instance.AddScore(item.CoinScore);
                    break;
                
                case Item.ItemType.Power:
                    if(playerShooting.Power == playerShooting.MaxPower)
                        GameManager.Instance.AddScore(item.PowerScore);
                    else
                        playerShooting.IncreasePower();
                    break;
                
                case Item.ItemType.Boom:
                    boomEffect.SetActive(true);
                    StartCoroutine(OffBoomEffect());
                    DestroyAllEnemies();
                    DestroyAllBullets();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    private IEnumerator OffBoomEffect()
    {
        yield return new WaitForSeconds(0.37f);
        boomEffect.SetActive(false);
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamaged(1000);
        }
    }

    private void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}
