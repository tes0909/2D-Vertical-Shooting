using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : IState
{
    protected BossStateMachine stateMachine;
    
    public BossBaseState(BossStateMachine bossStateMachine)
    {
        stateMachine = bossStateMachine;
    }
    
    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Exit()
    {
        
    }
    
    public void TakeDamaged(int damage)
    {
        if(stateMachine.CurrentHealth <= 0) return;
        stateMachine.CurrentHealth -= damage;
        
        stateMachine.Boss.Animator.SetTrigger("Hit"); // TODO: 문자열 조회 수정할 것

        if (stateMachine.CurrentHealth <= 0)
        {
            GameManager.Instance.AddScore(stateMachine.BossScore);
            Object.Destroy(stateMachine.Boss.gameObject);
            // 추가적으로 고려할 수 있는 것(보스 처치 이펙트 → Instantiate(폭발이펙트, 위치) 후 Destroy)
            // 씬 전환, 게임 클리어 처리 → GameManager.Instance.GameClear() 같은 호출
        }
    }
}
