using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Bullet bullet = other.GetComponent<Bullet>();
        if (enemy || bullet)
        {
            GameManager.Instance.PlayerRespawn();
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
