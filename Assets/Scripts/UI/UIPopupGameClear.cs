using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupGameClear : UIPopup
{
    [SerializeField] private Button clearButton;
    private readonly string mainScene = "MainScene";

    public override void Init()
    {
        base.Init();
        clearButton.onClick.AddListener(Clear);
    }

    private void Clear()
    {
        SceneManager.LoadScene(mainScene);
        UIManager.Instance.ClosePopup<UIPopupGameClear>();
    }
}
