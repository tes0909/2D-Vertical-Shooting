using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterState : BossBaseState
{
    private float timer;
    private float enterDuration = 2f;

    public BossEnterState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Boss.Rb2d.velocity = Vector2.down * stateMachine.Speed;
    }

    public override void Execute()
    {
        base.Execute();

        timer += Time.deltaTime;

        if (timer >= enterDuration)
        {
            stateMachine.Boss.Rb2d.velocity = Vector2.zero;
            stateMachine.ChangeState(stateMachine.ThinkState);
        }
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
