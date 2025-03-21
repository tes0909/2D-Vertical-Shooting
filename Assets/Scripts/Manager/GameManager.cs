using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [Header("Player Settings")] 
    [SerializeField] private GameObject player;

    [SerializeField] private int playerLife;
    public int PlayerLife { get => playerLife; private set => playerLife = value; }
    public event Action<int> OnLifeChanged;
    
    [SerializeField] private int playerScore;
    public int PlayerScore { get; private set; }
    public event Action<int> OnScoreChanged; 
    
    [Header("Enemy Settings")]
    [SerializeField] private List<GameObject> enemies;
    
    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float curSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float minSpawnTimeRange = 0.5f;
    [SerializeField] private float maxSpawnTimeRange = 3f;
    
    [Header("Default Settings")]
    [SerializeField] private GameObject gameOverPanel;
    private Button Restart;

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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Restart = gameOverPanel.GetComponentInChildren<Button>();
        Restart.onClick.AddListener(GameRetry);
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
        
        GameObject enemy = Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        
        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.player = player;
        
        switch (randomSpawnPoint)
        {
            case 5:
            case 6:
                enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
                rb2d.velocity = new Vector2(enemyClass.speed * 1, 1);
                break;
            case 7:
            case 8:
                enemy.transform.rotation = Quaternion.Euler(0, 0, -90);
                rb2d.velocity = new Vector2(enemyClass.speed * -1, 1);
                break;
            default:
                rb2d.velocity = Vector2.down * enemyClass.speed;
                break;
        }
    }

    public void PlayerRespawn()
    {
        StartCoroutine(PlayerRespawnCoroutine());
    }

    private IEnumerator PlayerRespawnCoroutine()
    {
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(0, -3.5f);
        player.gameObject.SetActive(true);
    }

    public void AddScore(int score)
    {
        playerScore += score;
        OnScoreChanged?.Invoke(playerScore);
        Debug.Log($"현재 점수 : {playerScore}");
    }

    public void RemoveLife()
    {
        playerLife--;
        OnLifeChanged?.Invoke(playerLife);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void GameRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
