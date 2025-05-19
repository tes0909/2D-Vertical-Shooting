using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArcState : BossBaseState
{
    public FireArcState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        stateMachine.Boss.RunCoroutine(FireArcCoroutine());
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator FireArcCoroutine()
    {
        stateMachine.CurPattenCount++;

        FireArc();
        
        if (stateMachine.CurPattenCount < stateMachine.MaxPattenCount[stateMachine.PatternIndex])
        {
            yield return new WaitForSeconds(0.15f);
            stateMachine.ChangeState(stateMachine.FireArcState);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            stateMachine.ChangeState(stateMachine.ThinkState);
        }
    }

    void FireArc()
    {
        FireArcShoot(ObjectManager.PoolType.BossBullet1, stateMachine.Boss.transform.position, 5);
    }
    
    private void FireArcShoot(ObjectManager.PoolType bulletPrefab, Vector3 position, int bulletSpeed)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * stateMachine.CurPattenCount / stateMachine.MaxPattenCount[stateMachine.PatternIndex]), -1);
        rb.velocity = direction.normalized * bulletSpeed;
    }
}
