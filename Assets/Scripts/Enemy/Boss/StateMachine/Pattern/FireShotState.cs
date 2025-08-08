using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotState : BossBaseState
{
    public FireShotState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Boss.RunCoroutine(FireShotCoroutine());
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    IEnumerator FireShotCoroutine()
    {
        stateMachine.CurPattenCount++;

        FireShot();
        
        if (stateMachine.CurPattenCount < stateMachine.MaxPattenCount[stateMachine.PatternIndex])
        {
            yield return new WaitForSeconds(3.5f);
            stateMachine.ChangeState(stateMachine.FireShotState);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            stateMachine.ChangeState(stateMachine.ThinkState);
        }
    }

    void FireShot()
    {
        for (int i = 0; i < 5; i++)
        {
            FireShotShoot(ObjectManager.PoolType.BossBullet2, stateMachine.Boss.transform.position, 3);
        }
    }
    
    private void FireShotShoot(ObjectManager.PoolType bulletPrefab, Vector3 position, int bulletSpeed)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle * 2f;
        Vector2 direction = (stateMachine.Boss.Target.transform.position - position).normalized;
        direction += randomDirection;
        rb.velocity = direction.normalized * bulletSpeed;
    }
}
