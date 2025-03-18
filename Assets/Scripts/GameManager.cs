using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [Header("Enemy Settings")]
    [SerializeField] private List<GameObject> enemies;
    
    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float curSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float minSpawnTimeRange = 0.5f;
    [SerializeField] private float maxSpawnTimeRange = 3f;
    

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        curSpawnTime += Time.deltaTime;

        if (curSpawnTime > maxSpawnTime)
        {
            SpawnEnemy();
            maxSpawnTime = Random.Range(minSpawnTimeRange, maxSpawnTimeRange);
            curSpawnTime = 0;
        }
    }

    private void SpawnEnemy()
    {
        var randomEnemy = Random.Range(0, enemies.Count);
        var randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
    }
}
