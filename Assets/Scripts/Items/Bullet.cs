using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string BorderBullet = "BorderBullet";
    public int damage;
    public bool isRotate;
    private float angle = 10f;
    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(BorderBullet))
            GetComponent<ReturnObject>()?.ReturnObj();
    }
}
