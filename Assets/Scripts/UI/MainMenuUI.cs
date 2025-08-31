using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    public override void Init()
    {
        base.Init();
        startButton.onClick.AddListener(GameStart);
        settingButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(GameEnd);
        Debug.Log("MainMenuUI Init");
    }

    private void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OpenSettings()
    {
        UIManager.Instance.OpenPopup<UIPopupSetting>();
    }
    
    private void GameEnd()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
