using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    /// <summary> 필요 시 초기화 작업 </summary>
    public virtual void Init() { }

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