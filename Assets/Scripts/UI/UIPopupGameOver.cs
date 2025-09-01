using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupGameOver : UIPopup
{
    [SerializeField] private Button restartButton;

    public override void Init()
    {
        base.Init();
        restartButton.onClick.AddListener(GameRetry);
    }

    private void GameRetry()
    {
        InGameManager.Instance.GameRetry();
        SoundManager.Instance.PlayBGM("BackgroundMusic");
    }
}
