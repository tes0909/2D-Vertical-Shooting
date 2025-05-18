using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] lifeImages;
    [SerializeField] private Image[] boomImages;
    
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
        scoreText.text = $"점수: {score:N0}";
    }

    private void LifeUpdateUI(int lifePoints)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].gameObject.SetActive(i < lifePoints);
        }
    }

    public void BoomUpdateUI(int boomPoints)
    {
        for (int i = 0; i < boomImages.Length; i++)
        {
            boomImages[i].gameObject.SetActive(i < boomPoints);
        }
        Debug.Log($"LifePoint: {boomPoints}");
    }
}
