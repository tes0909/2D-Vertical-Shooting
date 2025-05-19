using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public Boss Boss { get; }
    public string PlayerBullet { get; private set; } = "PlayerBullet";
    public int BossScore { get; private set; } = 5000;
    public float Speed { get; private set; } = 1.5f;
    public int CurrentHealth { get; set; } = 3000;
    public int PatternIndex { get; set; } // 현재 실행 중인 보스 공격 패턴의 번호
    public int CurPattenCount { get; set; } // 현재 선택된 패턴 실행 횟수
    public int[] MaxPattenCount { get; set; } // 공격 패턴 최대 반복 횟수
    public BossBaseState CurrentState { get; private set; }
    public BossEnterState EnterState { get; private set; }
    public BossThinkState ThinkState { get; private set; }
    public FireForwardState FireForwardState { get; private set; }
    public FireShotState FireShotState { get; private set; }
    public FireArcState FireArcState { get; private set; }
    public FireAroundState FireAroundState { get; private set; }
    public BossStateMachine(Boss boss)
    {
        this.Boss = boss;
        CurrentState = new BossBaseState(this);
        EnterState = new BossEnterState(this);
        ThinkState = new BossThinkState(this);
        FireForwardState = new FireForwardState(this);
        FireShotState = new FireShotState(this);
        FireArcState = new FireArcState(this);
        FireAroundState = new FireAroundState(this);
            
        PatternIndex = -1;
        MaxPattenCount = new int[] { 2, 3, 99, 10 };
    }
}
