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
    
    private readonly float[] _offsets = { 0f, 0.25f, 0.45f };
    private PlayerInputReceiver _playerInputReceiver;
    
    void Awake()
    {
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();
    }

    private void OnEnable()
    {
        _playerInputReceiver.OnShootEvent += Shooting;
    }

    private void OnDisable()
    {
        _playerInputReceiver.OnShootEvent -= Shooting;
    }

    private void Update()
    {
        Reload();
    }

    private void Shooting()
    {
        if (curShotDelay < maxShotDelay) return;

        switch (power)
        {
            case 1:
                Shoot(playerBulletPrefabA, transform.position, quaternion.identity);
                break;
            case 2:
                Shoot(playerBulletPrefabA, transform.position + Vector3.left * _offsets[1], Quaternion.identity);
                Shoot(playerBulletPrefabA, transform.position + Vector3.right * _offsets[1], Quaternion.identity);
                break;
            case 3:
                Shoot(playerBulletPrefabA, transform.position + Vector3.left * _offsets[2], Quaternion.identity);
                Shoot(playerBulletPrefabB, transform.position, quaternion.identity);
                Shoot(playerBulletPrefabA, transform.position + Vector3.right * _offsets[2], Quaternion.identity);
                break;
        }
        curShotDelay = 0;
    }
    
    private void Shoot(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed;
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
