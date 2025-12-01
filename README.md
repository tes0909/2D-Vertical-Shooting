# [프로젝트 소개] SKY 2025
# 1. 개요
+ 장르 : 2D 종스크롤 슈팅게임
+ 플랫폼 : PC(Windows), 모바일
+ 개발 기간 : 2025.04 ~ 2025.06
+ 개발 인원 : 1명
### 게임 소개
  > 
> 고전 2D 탄막 슈팅 게임 스트라이커즈 1945를
현대적으로 재해석하여 제작하였습니다.
> 
> 플레이어는 스테이지에 진입해 위에서 내려오는 정체불명의 적 비행체를 처치하고
아이템을 획득하여 마지막에 등장하는 보스를 격파하는 것을 목표로 합니다.

### 인게임
![Image](https://github.com/user-attachments/assets/425e7d1c-870f-4716-aa61-0969c4a63294)
![Image](https://github.com/user-attachments/assets/5f04aef4-bb8c-414e-a9f8-c1674c157ad2)

### [조작방법] ※ PC 버전 기준.
+ 이동: W, A, S, D
+ 공격: 마우스 왼쪽 클릭
+ 필살기 : Space

# 2. 주요 기술
### UGS 로그인 구현
Unity Gaming Services(Authentication)를 활용하여 회원가입(SignUp), 로그인(Login)을 담당하는 클래스입니다.
SingletonDontDestroy 기반으로 전역에서 접근 가능하며 씬 전환에도 유지됩니다.
```csharp
using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationManager : SingletonDontDestroy<AuthenticationManager>, IBaseManager
{
    public bool IsInitialized { get; private set; }

    public void Init()
    {
        IsInitialized = true;
        Initialize();
    }

    private async void Initialize()
    {
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized");
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void Login(TMP_InputField idField, TMP_InputField passwordField)
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }

        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(id, password);
            SceneManager.LoadScene("MainScene");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    public async void SignUp(TMP_InputField idField, TMP_InputField passwordField)
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }
        
        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(id, password);
            SceneManager.LoadScene("MainScene");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
```
- 싱글톤 기반 매니저 구조
  - SingletonDontDestroy<T>를 상속하여 씬 전환(Scene Load)시에도 파괴되지 않고 유지됩니다.
- Unity Services 초기화
  - UnityServices.InitializeAsync() 실행하여 UGS 기능 활성화
  - 초기화 실패 시 try-catch로 예외 처리하였습니다.
- 로그인(Login)
  - 아이디, 비밀번호 입력값을 받아 공백 여부를 검증합니다. 
  - SignInWithUsernamePasswordAsync(id, password)를 호출해 UGS 인증 서버에 로그인 요청을 보냅니다
  - 인증 성공 시 MainScene으로 이동합니다.
  - 발생 가능 예외:
    - AuthenticationException(잘못된 계정 정보 등 인증 실패) / RequestFailedException(네트워크, 서버 불안정 등 요청 실패) 예외 처리 하였습니다.
- 회원가입(SignUp)
  - SignUpWithUsernamePasswordAsync(id, password) 사용하여 성공 시 자동으로 MainScene 이동합니다.
  - 로그인과 동일한 예외 처리 구조

### 플레이어 & 적 이동, 공격(옵저버 패턴)
```Csharp
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnShootEvent;
    public event Action OnBoomEvent;

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        OnMoveEvent?.Invoke(input);
    }

    public void OnShoot(InputValue value)
    {
        OnShootEvent?.Invoke();
    }

    public void OnBoom(InputValue value)
    {
        OnBoomEvent?.Invoke();
    }
}
```
- 옵저버 패턴
  - Unity Input System 기반으로 사용자 입력을 받아 델리게이트를 통해 입력 이벤트를 외부로 전달하였습니다.

### 총알 & 몬스터 & 아이템 최적화(오브젝트 풀링)
```CSharp
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public enum PoolType
    {
        EnemyMini1,
        EnemyElite1,
        EnemyElite2,
        PlayerBullet1,
        PlayerBullet2,
        EnemyBullet1,
        EnemyBullet2,
        ItemGoldCoin,
        ItemPowerUp,
        ItemBoom,
        BossBullet1,
        BossBullet2,
        BossBullet3,
        BossBullet4,
        EnemyBoss
    }
    
    [Serializable]
    public class Pool
    {
        public PoolType key;
        public GameObject prefab;
        public int size;
    }
    
    public List<Pool> PoolList;
    private Dictionary<PoolType, Queue<GameObject>> PoolDictionary = new Dictionary<PoolType, Queue<GameObject>>();
    private Dictionary<PoolType, List<GameObject>> ActiveObject = new Dictionary<PoolType, List<GameObject>>(); // 활성화 오브젝트 리스트

    protected override void Awake()
    {
        base.Awake();
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var pool in PoolList)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            List<GameObject> list = new List<GameObject>(); // 활성화 오브젝트(List) 타입 생성
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            PoolDictionary.Add(pool.key, queue);
            ActiveObject.Add(pool.key, list);
        }
    }

    public GameObject GetObject(PoolType key)
    {
        if (PoolDictionary.ContainsKey(key) && PoolDictionary[key].Count > 0)
        {
            GameObject obj = PoolDictionary[key].Dequeue();
            obj.SetActive(true);
            ActiveObject[key].Add(obj);
            return obj;
        }
        return null;
    }

    public void ReturnObject(PoolType key, GameObject obj)
    {
        if(!PoolDictionary.ContainsKey(key)) return;
        
        obj.SetActive(false);
        PoolDictionary[key].Enqueue(obj);
        ActiveObject[key].Remove(obj);
    }

    // 해당 풀 타입의 모든 오브젝트를 가져옴
    public GameObject[] GetObjects(PoolType key)
    {
        return ActiveObject[key].ToArray(); // 큐를 배열로 반환
    }
}
```
- 풀 타입 Enum 관리 (PoolType)
  - Enemy, Bullet, Item, Boss 등 다양한 종류의 오브젝트를 고정된 Enum 키로 식별합니다.
- 초기 풀 생성 — InitializePool
  - 풀링 시스템 초기화 시 다음 작업 수행:
    - PoolList로부터 각 PoolType별 Prefab과 초기 생성 개수(size)를 확인합니다.
    - GameObject를 Pool의 size만큼 Instantiate하여 비활성화 상태로 Queue에 저장합니다.
    - 활성화된 오브젝트를 별도로 관리하기 위해 ActiveObject List 초기화
    - 미리 생성해 둠으로써 Instantiate 비용을 줄여 런타임 성능이 향상되었습니다.
- 오브젝트 가져오기 — GetObject(PoolType key)
  - 오브젝트가 저장된 PoolDictionary[key]에서 Dequeue()하여 오브젝트를 꺼낸 뒤 SetActive(true)로 활성화 합니다.
  - 활성화 중인 오브젝트 리스트(ActiveObject)에 등록하고 오브젝트를 반환합니다.
  - 큐가 비어 있을 경우 null 반환
- 오브젝트 반환 — ReturnObject(PoolType key, GameObject obj)
  - 사용 완료된 오브젝트를 다시 비활성화하고 Queue에 다시 넣습니다.
  - ActiveObject 리스트에서도 제거합니다.
- 활성화된 오브젝트 조회 — GetObjects(PoolType key)
  - 특정 풀 타입의 현재 활성 중인 모든 오브젝트 배열을 반환합니다.

### 보스 패턴 구현
  + FSM 패턴 활용(Boss 행동을 상태(State) 단위로 관리)
### 저장, 로드, 커스텀 배치
  + JsonUtility & DataManager 활용
### 적 기체 공격
  + 상속 활용
  + 유연한 확장성
### 점수, 라이프
  + GameManager Event를 통해 UIManager에서 업데이트

### 아이템 구현(코인, 파워업, 폭발)

# 3. 에셋
+ Aesprite 제작, 일부 에셋은 Unity Asset Store 사용
  
# 4. 개발환경
### 데이터 관리
+ Json

### 개발 도구 & 언어
+ Github Desktop, Unity  
+ C#

### 개발 환경
+ Unity 2022.3.62f2
+ Visual Studio, Rider
+ Windows10, 11
