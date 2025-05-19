using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireForwardState : BossBaseState
{
    public FireForwardState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Boss.RunCoroutine(FireForwardCoroutine());
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator FireForwardCoroutine()
    {
        stateMachine.CurPattenCount++;

        FireForward();
        
        if (stateMachine.CurPattenCount < stateMachine.MaxPattenCount[stateMachine.PatternIndex])
        {
            yield return new WaitForSeconds(2f);
            stateMachine.ChangeState(stateMachine.FireForwardState);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            stateMachine.ChangeState(stateMachine.ThinkState);
        }
    }

    void FireForward()
    {
        FireForwardShoot(ObjectManager.PoolType.BossBullet3, stateMachine.Boss.transform.position + Vector3.left * 0.3f, 4);
        FireForwardShoot(ObjectManager.PoolType.BossBullet3, stateMachine.Boss.transform.position + Vector3.left * 0.45f, 4);
        FireForwardShoot(ObjectManager.PoolType.BossBullet3, stateMachine.Boss.transform.position + Vector3.right * 0.3f, 4);
        FireForwardShoot(ObjectManager.PoolType.BossBullet3, stateMachine.Boss.transform.position + Vector3.right * 0.45f, 4);
    }
    
    private void FireForwardShoot(ObjectManager.PoolType bulletPrefab, Vector3 position, int bulletSpeed)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
    }
}
