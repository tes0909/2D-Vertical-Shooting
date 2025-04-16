using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        startButton.onClick.AddListener(GameStart);
        settingButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(GameEnd);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OpenSettings()
    {
        Debug.Log("설정창 열림");   
    }
    
    private void GameEnd()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
