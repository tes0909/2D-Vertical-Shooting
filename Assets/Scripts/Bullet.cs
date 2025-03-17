using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string borderBullet = "BorderBullet";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(borderBullet))
            Destroy(gameObject);
    }
}
