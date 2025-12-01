# [프로젝트 소개] SKY 2025
# 1. 개요
+ 장르 : 2D 슈팅게임
+ 플랫폼 : PC(Windows)
+ 개발 기간 : 2025.04 ~ 진행중
+ 개발 인원 : 1명
+ 게임 소개
  > 
> 고전 2D 탄막 슈팅 게임 스트라이커즈 1945를
2025년에 재탄생시킨 컨셉으로 제작하였습니다.
> 
> 플레이어는 맵에 입장하여 위에서 내려오는 원인모를 비행체를 처치하고
아이템을 획득하여 마지막에 등장하는 보스를 처치하는 것을 목표로 합니다.

### 인게임
![Image](https://github.com/user-attachments/assets/425e7d1c-870f-4716-aa61-0969c4a63294)
![Image](https://github.com/user-attachments/assets/5f04aef4-bb8c-414e-a9f8-c1674c157ad2)

### [조작방법] ※ PC 버전 기준.
+ 이동: W, A, S, D
+ 공격: 마우스 왼쪽 클릭
+ 필살기 : Space

# 2. 구현 기술
### UGS 로그인 구현
### 보스 패턴 구현
  + FSM 패턴 활용
### 플레이어 & 적 이동, 공격
  + 옵저버 패턴 활용
### 저장, 로드, 커스텀 배치
  + JsonUtility & DataManager 활용
### 적 기체 공격
  + 상속 활용
  + 유연한 확장성
### 점수, 라이프
  + GameManager Event를 통해 UIManager에서 업데이트

### 총알 & 몬스터 & 아이템 최적화
  + 오브젝트 풀링

### 아이템 구현(코인, 파워업, 폭발)

# 3. 에셋
+ Aesprite 제작, 에셋스토어 다운로드
  
# 4. 개발환경
### 데이터 관리
+ Json

### 개발 도구 & 언어
+ Github Desktop, Unity  
+ C#

### 개발 환경
+ Unity 2022.3.17f1
+ Visual Studio, Rider
+ Windows10, 11
