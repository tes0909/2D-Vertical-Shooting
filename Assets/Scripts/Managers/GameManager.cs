using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [FormerlySerializedAs("player")]
    [Header("Player Settings")] 
    [SerializeField] private GameObject target;

    [SerializeField] private int playerLife = 3;
    public int PlayerLife => playerLife;
    public event Action<int> OnLifeChanged;
    
    [SerializeField] private int playerScore;
    public int PlayerScore => playerScore;
    public event Action<int> OnScoreChanged; 
    
    [Header("Enemy Settings")]
    [SerializeField] private List<GameObject> enemies;
    public DataManager.MonsterDataList monsterDataList;
    
    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float curSpawnTime;
    [SerializeField] private int currentSpawnIndex;
    
    [Header("Default Settings")]
    [SerializeField] private GameObject gameOverPanel;
    private Button Restart;

    private void Start()
    {
        Restart = gameOverPanel.GetComponentInChildren<Button>();
        Restart.onClick.AddListener(GameRetry);
        
        DataManager.Instance.SaveData(monsterDataList);
        //monsterDataList = DataManager.Instance.LoadData<DataManager.MonsterDataList>();
    }

    private void Update()
    {
        if(monsterDataList == null || monsterDataList.monsterData == null || currentSpawnIndex >= monsterDataList.monsterData.Count) return;
        curSpawnTime += Time.deltaTime;

        var next = monsterDataList.monsterData[currentSpawnIndex];
        if (curSpawnTime > next.spawnDelay)
        {
            SpawnEnemy(next);
            curSpawnTime = 0;
            currentSpawnIndex++;
        }
    }

    private void SpawnEnemy(DataManager.MonsterData monsterData)
    {
        GameObject enemy = ObjectManager.Instance.GetObject(monsterData.type);
        enemy.transform.position = spawnPoints[monsterData.spawnPoint].position;
        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();

        if (monsterData.type == ObjectManager.PoolType.EnemyBoss) // 보스
        {
            Boss boss = enemy.GetComponent<Boss>();
            boss.Target = target;
        }
        else // 일반 몬스터
        {
            Enemy enemyClass = enemy.GetComponent<Enemy>();
            enemyClass.Target = target;
        
            switch (monsterData.spawnPoint)
            {
                case 5:
                case 6:
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
                    rb2d.velocity = new Vector2(enemyClass.Speed * 1, 1);
                    break;
                case 7:
                case 8:
                    enemy.transform.rotation = Quaternion.Euler(0, 0, -90);
                    rb2d.velocity = new Vector2(enemyClass.Speed * -1, 1);
                    break;
                default:
                    rb2d.velocity = Vector2.down * enemyClass.Speed;
                    break;
            }
        }
    }

    public void PlayerRespawn()
    {
        StartCoroutine(PlayerRespawnCoroutine());
    }

    private IEnumerator PlayerRespawnCoroutine()
    {
        yield return new WaitForSeconds(2f);
        target.transform.position = new Vector3(0, -3.5f);
        target.gameObject.SetActive(true);

        // 중복 피격 방지
        PlayerCollision playerCollision = target.GetComponent<PlayerCollision>();
        playerCollision.isDamaged = false;
    }

    public void AddScore(int score)
    {
        playerScore += score;
        OnScoreChanged?.Invoke(playerScore);
    }

    public void RemoveLife()
    {
        playerLife--;
        OnLifeChanged?.Invoke(playerLife);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void GameRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
