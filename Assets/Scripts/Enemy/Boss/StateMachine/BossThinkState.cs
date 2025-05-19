using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThinkState : BossBaseState
{
    public BossThinkState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Think();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    protected virtual void Think()
    {
        stateMachine.PatternIndex = (stateMachine.PatternIndex + 1) % 4;
        stateMachine.CurPattenCount = 0;
        switch (stateMachine.PatternIndex)
        {
            case 0:
                stateMachine.ChangeState(stateMachine.FireForwardState);
                break;
            case 1:
                stateMachine.ChangeState(stateMachine.FireShotState);
                break;
            case 2:
                stateMachine.ChangeState(stateMachine.FireArcState);
                break;
            case 3:
                stateMachine.ChangeState(stateMachine.FireAroundState);
                break;
        }
    }
}
