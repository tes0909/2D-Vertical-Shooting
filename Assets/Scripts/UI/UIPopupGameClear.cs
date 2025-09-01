using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupGameClear : UIPopup
{
    [SerializeField] private Button clearButton;
    private readonly string startScene = "StartScene";

    public override void Init()
    {
        base.Init();
        clearButton.onClick.AddListener(Clear);
    }

    private void Clear()
    {
        SceneManager.LoadScene(startScene);
        UIManager.Instance.ClosePopup<UIPopupGameClear>();
    }
}
