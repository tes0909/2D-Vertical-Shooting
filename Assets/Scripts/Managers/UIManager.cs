using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 생성과 제거를 중앙에서 관리하는 싱글톤 매니저
/// </summary>
public class UIManager : Singleton<UIManager>
{ 
    private Dictionary<string, UIBase> uiDict = new();

    /// <summary>
    /// 팝업 열기 (보이기)
    /// </summary>
    public T OpenPopup<T>() where T : UIBase
    { 
        string uiName = typeof(T).Name; // 제네릭 타입 T의 클래스 이름을 문자열로 가져옴
        
        // 이미 있는 경우 재사용
        if (uiDict.TryGetValue(uiName, out var existing))
        {
            existing.Open();
            return (T)existing;
        }
        
        UIBase prefab = Resources.Load<UIBase>($"UI/{uiName}");
        
        UIBase ui = Util.InstantiateUI(prefab, transform);
        ui.name = uiName;
        ui.Open();
        uiDict.Add(uiName, ui);

        return (T)ui;
    }

    /// <summary>
    /// 제네릭으로 팝업 닫기
    /// </summary>
    public void ClosePopup<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;
        ClosePopup(uiName);
    }

    /// <summary>
    /// 이름으로 팝업 닫기
    /// </summary>
    private void ClosePopup(string uiName)
    {
        if (uiDict.TryGetValue(uiName, out var ui))
        {
            ui.Close();
        }
    }

    public T GetUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        if (uiDict.TryGetValue(uiName, out var ui))
        {
            return (T)ui;
        }
        Debug.LogError($"{uiName} is not found");
        return null;
    }
}