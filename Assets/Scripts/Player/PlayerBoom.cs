using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    private Player player;
    public int CurrentBoom { get; private set; } = 1;
    public int MaxBoom { get; private set; } = 2;
    public bool isBoomActive;
    [SerializeField] private GameObject boomEffect;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    
    private void OnEnable()
    {
        StartCoroutine(BoomEvent());
    }

    IEnumerator BoomEvent()
    {
        while (player == null || player.PlayerInputReceiver == null)
            yield return null;
        player.PlayerInputReceiver.OnBoomEvent += ActiveBoom;    
    }
    
    private void OnDisable()
    {
        player.PlayerInputReceiver.OnBoomEvent -= ActiveBoom;
    }

    private void ActiveBoom()
    {
        if(isBoomActive) return; // 현재 활성화 중인경우
        
        if(CurrentBoom == 0) return;

        DecreaseBoom();
        isBoomActive = true;
        
        boomEffect.SetActive(true);
        StartCoroutine(OffBoomEffect());
        
        DestroyAllEnemies();
        DestroyAllBullets();
    }
    
    public void IncreaseBoom()
    {
        CurrentBoom++;
        UIManager.Instance.BoomUpdateUI(CurrentBoom);
    }

    private void DecreaseBoom()
    {
        CurrentBoom--;
        UIManager.Instance.BoomUpdateUI(CurrentBoom);
    }
    
    private IEnumerator OffBoomEffect()
    {
        yield return new WaitForSeconds(0.4f);
        boomEffect.SetActive(false);
        isBoomActive = false;
    }

    private void DestroyAllEnemies()
    {
        ObjectManager.PoolType[] enemyTypes =
        {
            ObjectManager.PoolType.EnemyMini1, ObjectManager.PoolType.EnemyElite1, ObjectManager.PoolType.EnemyElite2
        }; // 적 종류
        
        foreach (var type in enemyTypes)
        { 
            GameObject[] enemies = ObjectManager.Instance.GetObjects(type); // 해당 타입에 해당되는 enemy 모두 가져옴

            foreach (var enemy in enemies) // 배열로 가져온 enemy 각 개체에 접근 
            {
                if (enemy.activeSelf)
                {
                    enemy.GetComponent<EnemyHealth>()?.TakeDamaged(int.MaxValue);
                }
            }
        }
    }

    private void DestroyAllBullets()
    {
        ObjectManager.PoolType[] bulletTypes =
        {
            ObjectManager.PoolType.EnemyBullet1, ObjectManager.PoolType.EnemyBullet2
        };

        foreach (var type in bulletTypes)
        {
            GameObject[] bullets = ObjectManager.Instance.GetObjects(type);

            foreach (var bullet in bullets)
            {
                if (bullet.activeSelf)
                {
                    bullet.GetComponent<ReturnObject>()?.ReturnObj();
                }
            }
        }
    }
}
