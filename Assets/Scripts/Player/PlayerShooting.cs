using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float curShotDelay;
    [SerializeField] private float maxShotDelay;

    public int Power { get; private set; } = 1;
    public int MaxPower { get; private set; } = 3;
    
    private readonly float[] _offsets = { 0f, 0.25f, 0.45f };

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        StartCoroutine(ShootEvent());
    }

    IEnumerator ShootEvent()
    {
        while (player == null || player.PlayerInputReceiver == null)
            yield return null;
        player.PlayerInputReceiver.OnShootEvent += Shooting;    
    }

    private void OnDisable()
    {
        player.PlayerInputReceiver.OnShootEvent -= Shooting;
    }

    private void Update()
    {
        Reload();
    }
    

    private void Shooting()
    {
        if (curShotDelay < maxShotDelay) return;

        switch (Power)
        {
            case 1:
                Shoot(ObjectManager.PoolType.PlayerBullet1, transform.position, quaternion.identity);
                break;
            case 2:
                Shoot(ObjectManager.PoolType.PlayerBullet1, transform.position + Vector3.left * _offsets[1], Quaternion.identity);
                Shoot(ObjectManager.PoolType.PlayerBullet1, transform.position + Vector3.right * _offsets[1], Quaternion.identity);
                break;
            case 3:
                Shoot(ObjectManager.PoolType.PlayerBullet1, transform.position + Vector3.left * _offsets[2], Quaternion.identity);
                Shoot(ObjectManager.PoolType.PlayerBullet2, transform.position, quaternion.identity);
                Shoot(ObjectManager.PoolType.PlayerBullet1, transform.position + Vector3.right * _offsets[2], Quaternion.identity);
                break;
        }
        curShotDelay = 0;
    }
    
    private void Shoot(ObjectManager.PoolType bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed;
    }


    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void IncreasePower()
    {
        if(Power < MaxPower)
            Power++;
    }
}
