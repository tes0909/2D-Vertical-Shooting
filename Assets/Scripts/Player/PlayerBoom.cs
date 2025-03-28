using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    private PlayerInputReceiver _playerInputReceiver;
    
    public int CurrentBoom { get; private set; }
    public int MaxBoom { get; private set; }
    public bool isBoomActive;
    [SerializeField] private GameObject boomEffect;
    public event Action<int> OnBoomChanged;
    void Awake()
    {
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();
        CurrentBoom = 0;
        MaxBoom = 2;
    }

    private void OnEnable()
    {
        _playerInputReceiver.OnBoomEvent += ActiveBoom;
    }
    
    private void OnDisable()
    {
        _playerInputReceiver.OnBoomEvent -= ActiveBoom;
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
