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
            
            InGameManager.Instance.RemoveLife();

            if (InGameManager.Instance.PlayerLife == 0)
            {
                SoundManager.Instance.PlaySFX("Plane_Boom");
                InGameManager.Instance.GameOver();
            }
            else
            {
                SoundManager.Instance.PlaySFX("Plane_Boom");
                InGameManager.Instance.PlayerRespawn();
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
                    InGameManager.Instance.AddScore(item.CoinScore);
                    break;
                
                case Item.ItemType.Power:
                    if(playerShooting.Power == playerShooting.MaxPower)
                        InGameManager.Instance.AddScore(item.PowerScore);
                    else
                        playerShooting.IncreasePower();
                    break;
                
                case Item.ItemType.Boom:
                    if (playerBoom.CurrentBoom == playerBoom.MaxBoom)
                        InGameManager.Instance.AddScore(item.BoomScore);
                    else
                        playerBoom.IncreaseBoom();
                    break;
            }
            SoundManager.Instance.PlaySFX("Item_Pickup");
            other.GetComponent<ReturnObject>()?.ReturnObj();
        }
    }
    
    // 무적
    public IEnumerator InvincibleCoroutine(float duration)
    {
        isDamaged = true; // 무적 시작 → 충돌 무시
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float elapsed = 0;
        while (elapsed < duration)
        {
            sr.enabled = !sr.enabled; // 깜빡임
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        sr.enabled = true;
        isDamaged = false; // 무적 해제
    }

}
