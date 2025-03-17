using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefabA;
    [SerializeField] private GameObject playerBulletPrefabB;
    
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float curShotDelay;
    [SerializeField] private float maxShotDelay;
    [SerializeField] private int power;
    
    private PlayerInputReceiver _playerInputReceiver;
    
    void Awake()
    {
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();
    }

    private void OnEnable()
    {
        _playerInputReceiver.OnShootEvent += ReloadShoot;
    }

    private void OnDisable()
    {
        _playerInputReceiver.OnShootEvent -= ReloadShoot;
    }

    private void Update()
    {
        curShotDelay += Time.deltaTime;
    }

    private void ReloadShoot()
    {
        if (curShotDelay < maxShotDelay) return;

        switch (power)
        {
            case 1:
                Shoot(playerBulletPrefabA, transform.position, quaternion.identity);
                break;
            case 2:
                Shoot(playerBulletPrefabA, transform.position + Vector3.left * 0.1f, Quaternion.identity);
                Shoot(playerBulletPrefabA, transform.position + Vector3.right * 0.1f, Quaternion.identity);
                break;
            case 3:
                Shoot(playerBulletPrefabA, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Shoot(playerBulletPrefabB, transform.position, quaternion.identity);
                Shoot(playerBulletPrefabA, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
        }
        curShotDelay = 0;
    }
    
    private void Shoot(GameObject bulletPrefab, Vector3 position,Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed;
    }
}
