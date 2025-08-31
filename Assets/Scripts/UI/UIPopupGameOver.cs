using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupGameOver : UIPopup
{
    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(InGameManager.Instance.GameRetry);
    }
}
