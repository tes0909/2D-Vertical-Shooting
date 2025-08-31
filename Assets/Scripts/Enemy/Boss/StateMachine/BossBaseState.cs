using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : IState
{
    private static readonly int Hit = Animator.StringToHash("Hit");
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
        
        stateMachine.Boss.Animator.SetTrigger(Hit);

        if (stateMachine.CurrentHealth <= 0)
        {
            InGameManager.Instance.AddScore(stateMachine.BossScore);
            Object.Destroy(stateMachine.Boss.gameObject);
        }
    }
}
