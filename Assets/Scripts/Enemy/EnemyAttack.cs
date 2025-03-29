using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float curShotDelay;
    [SerializeField] private float maxShotDelay;
    [SerializeField] private float bulletSpeed = 10f;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Shooting();
        Reload();
    }
    
    private void Shooting()
    {
        if (curShotDelay < maxShotDelay) return;

        switch (_enemy.GetEnemyType())
        {
            case Enemy.EnemyType.Small:
                Shoot(ObjectManager.PoolType.EnemyBullet1, transform.position + new Vector3(-0.4f, 0.2f), Quaternion.identity);
                Shoot(ObjectManager.PoolType.EnemyBullet1, transform.position + new Vector3(0.4f, 0.2f), Quaternion.identity);
                break;
            case Enemy.EnemyType.Large:
                Shoot(ObjectManager.PoolType.EnemyBullet2, transform.position, Quaternion.identity);
                break;
        }
        curShotDelay = 0;
    }
    
    private void Shoot(ObjectManager.PoolType bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = _enemy.player.transform.position - transform.position;
        rb.velocity = direction.normalized * bulletSpeed;
    }
    
    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
