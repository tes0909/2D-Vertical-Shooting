using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] lifeImages;
    
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                
                if (_instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
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
    void Start()
    {
        // 점수 초기화
        ScoreUpdateUI(GameManager.Instance.PlayerScore);
        
        // UI 초기화
        LifeUpdateUI(GameManager.Instance.PlayerLife);
        
        GameManager.Instance.OnScoreChanged += ScoreUpdateUI;
        GameManager.Instance.OnLifeChanged += LifeUpdateUI;
    }
    

    private void ScoreUpdateUI(int score)
    {
        scoreText.text = $"점수: {score}";
    }

    private void LifeUpdateUI(int lifePoints)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].gameObject.SetActive(i < lifePoints);
        }

        Debug.Log($"LifePoint: {lifePoints}");
    }
}
