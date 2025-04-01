using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float curShotDelay;
    [SerializeField] protected float maxShotDelay;
    [SerializeField] protected float bulletSpeed = 10f;
    private Enemy _enemy;

    protected virtual void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected virtual void Update()
    {
        Shooting();
        Reload();
    }

    protected abstract void Shooting();
    
    protected void Shoot(ObjectManager.PoolType bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = _enemy.player.transform.position - transform.position;
        rb.velocity = direction.normalized * bulletSpeed;
    }
    
    protected void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
