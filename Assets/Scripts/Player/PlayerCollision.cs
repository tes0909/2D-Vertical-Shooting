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
            other.GetComponent<ReturnObject>()?.ReturnObj();
        }
        else if (other.gameObject.CompareTag(Items))
        {
            Item item = other.gameObject.GetComponent<Item>();
            PlayerShooting playerShooting = GetComponent<PlayerShooting>();
            PlayerBoom playerBoom = GetComponent<PlayerBoom>();
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
                    if (playerBoom.CurrentBoom == playerBoom.MaxBoom)
                        GameManager.Instance.AddScore(item.BoomScore);
                    else
                        playerBoom.IncreaseBoom();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

}
