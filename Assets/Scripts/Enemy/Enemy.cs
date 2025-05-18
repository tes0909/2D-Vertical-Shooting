using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public EnemyCollision EnemyCollision { get; private set; }
    public EnemyHealth EnemyHealth { get; private set; }
    public EnemyItemDrop EnemyItemDrop { get; private set; }
    public float Speed { get; private set; } = 3f;
    public GameObject Player { get; set; }
    
    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyCollision = GetComponent<EnemyCollision>();
        EnemyHealth = GetComponent<EnemyHealth>();
        EnemyItemDrop = GetComponent<EnemyItemDrop>();
    }
}
