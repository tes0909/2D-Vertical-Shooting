using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.RegisterUI(this);
    }

    /// <summary> 초기화 작업 </summary>
    public virtual void Init(){ }

    private void OnDestroy()
    {
        if (UIManager.Instance != null) 
        {
            UIManager.Instance.UnRegisterUI(this);
        }
    }

    /// <summary> UI가 화면에 표시될 때 실행됨 </summary>
    public virtual void Open(params object[] args)
    {
        gameObject.SetActive(true);
    }

    /// <summary> UI를 닫는 기본 동작 (Close 버튼 등에서 호출) </summary>
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}