using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 매니저가 구현해야 하는 기본 인터페이스
/// </summary>


public abstract class BaseGameManager : MonoBehaviour
{
    /// <summary> 초기화할 매니저들을 담는 리스트 </summary>
    protected List<IBaseManager> managers = new();
    
    /// <summary> 모든 매니저 초기화가 끝났는지 여부 </summary>
    public bool IsInitialized { get; private set; }
    public static BaseGameManager Instance { get; private set; }

    
    /// <summary>
    /// 게임 시작 시 가장 먼저 실행.
    /// 싱글톤 중복 방지, FPS 설정, 매니저 등록 실행.
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;

        AddManagers();
    }

    /// <summary>
    /// 상속받는 클래스에서 각 매니저 등록 로직 구현
    /// </summary>
    protected abstract void AddManagers();
    
    /// <summary>
    /// 모든 Awake 이후 Start에서 초기화 시작
    /// </summary>
    private void Start()
    {
        Debug.Log($"🔵 - [ {GetType().Name} ] Initialize Start!");
        StartCoroutine(Initialize());
    }
    
    // <summary>
    /// 초기화 전체 과정 실행 (순차 초기화 → 강제 초기화 → 완료 처리)
    /// </summary>
    private IEnumerator Initialize()
    {
        yield return StartCoroutine(InitializeManagers()); // 등록된 매니저 순차 초기화
        InitializeManagerForce();                          // 강제 초기화 매니저
        InitializeCompleted();                             // 완료 처리
    }

    /// <summary>
    /// 매니저 리스트 순서대로 Init 실행
    /// </summary>
    private IEnumerator InitializeManagers()
    {
        yield return null; // 첫 프레임 대기 (Awake 모두 실행 보장)

        foreach (var manager in managers)
        {
            manager.Init();
            yield return new WaitUntil(() => manager.IsInitialized);
        }
    }
    
    /// <summary>
    /// 특정 매니저를 강제로 먼저 초기화할 필요가 있을 때 구현
    /// (에디터 디버그용, 테스트용 매니저 등)
    /// </summary>
    protected abstract void InitializeManagerForce();

    /// <summary>
    /// 모든 매니저 초기화 완료 시 실행
    /// </summary>
    private void InitializeCompleted()
    {
        Debug.Log($"🔵 - [ {GetType().Name} ] Initialize Completed!");
        IsInitialized = true;
        OnInit();
    }

    /// <summary>
    /// 초기화 완료 후 게임 시작 시 실행할 로직
    /// </summary>
    protected abstract void OnInit();
}
