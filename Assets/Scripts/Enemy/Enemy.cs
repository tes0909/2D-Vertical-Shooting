using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Small,
        Large
    }
    
    public float speed;
    public GameObject player;
    
    [SerializeField] private EnemyType type;

    public EnemyType GetEnemyType()
    {
        return type;
    }
}
