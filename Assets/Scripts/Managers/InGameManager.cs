using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InGameManager : Singleton<InGameManager>
{
    [Header("Player Settings")] 
    [SerializeField] private GameObject player;
    [SerializeField] private int playerLife = 3;
    [SerializeField] private int playerScore;
    public int PlayerLife => playerLife;
    public int PlayerScore => playerScore;
    public event Action<int> OnLifeChanged;
    public event Action<int> OnScoreChanged; 
    
    [Header("Enemy Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    public DataManager.MonsterDataList monsterDataList;
    
    private float SpawnTimer;
    private int currentSpawnIndex;

    private void Start()
    {
        DataManager.Instance.SaveData(monsterDataList);
        //monsterDataList = DataManager.Instance.LoadData<DataManager.MonsterDataList>();
    }

    private void Update()
    {
        HandleEnemySpawn();
    }

    #region HandleEnemySpawning
    private void HandleEnemySpawn()
    {
        if(monsterDataList == null || monsterDataList.monsterData == null || currentSpawnIndex >= monsterDataList.monsterData.Count) return;
        SpawnTimer += Time.deltaTime;

        var next = monsterDataList.monsterData[currentSpawnIndex];
        if (SpawnTimer > next.spawnDelay)
        {
            SpawnEnemy(next);
            SpawnTimer = 0;
            currentSpawnIndex++;
        }
    }
    
    private void SpawnEnemy(DataManager.MonsterData monsterData)
    {
        GameObject enemy = ObjectManager.Instance.GetObject(monsterData.type);
        enemy.transform.position = spawnPoints[monsterData.spawnPoint].position;
        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();

        if (monsterData.type == ObjectManager.PoolType.EnemyBoss)
        {
            // 보스 처리
            enemy.GetComponent<Boss>().Target = player;
            return;
        }
        
        // 일반 몬스터 처리
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.Target = player;
        SetEnemyMovement(enemy, enemyClass.Speed, monsterData.spawnPoint);
    }
    
    private void SetEnemyMovement(GameObject enemy, float speed, int spawnPoint)
    {
        Rigidbody2D rb2d = enemy.gameObject.GetComponent<Rigidbody2D>();
        switch (spawnPoint)
        {
            case 5: case 6:
                enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
                rb2d.velocity = new Vector2(speed, 1);
                break;
            case 7: case 8:
                enemy.transform.rotation = Quaternion.Euler(0, 0, -90);
                rb2d.velocity = new Vector2(-speed, 1);
                break;
            default:
                rb2d.velocity = Vector2.down * speed;
                break;
        }
    }
    #endregion
    
    #region Player Management
    public void PlayerRespawn() => StartCoroutine(PlayerRespawnCoroutine());

    private IEnumerator PlayerRespawnCoroutine()
    {
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(0, -3.5f);
        player.gameObject.SetActive(true);

        // 중복 피격 방지
        PlayerCollision playerCollision = player.GetComponent<PlayerCollision>();
        playerCollision.isDamaged = false;
    }
    #endregion

    #region Score & Life
    public void AddScore(int score)
    {
        playerScore += score;
        OnScoreChanged?.Invoke(playerScore);
    }

    public void RemoveLife()
    {
        playerLife = Mathf.Max(0, playerLife - 1);
        OnLifeChanged?.Invoke(playerLife);
    }
    #endregion
    
    #region Game State
    public void GameOver()
    {
        UIManager.Instance.OpenPopup<UIPopupGameOver>();
        Time.timeScale = 0f;
    }

    public void GameRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
