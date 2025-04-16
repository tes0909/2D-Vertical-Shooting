using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string BorderBullet = "BorderBullet";
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(BorderBullet))
            GetComponent<ReturnObject>()?.ReturnObj();
    }
}
