using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAroundState : BossBaseState
{
    public FireAroundState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        stateMachine.Boss.RunCoroutine(FireAroundCoroutine());
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator FireAroundCoroutine()
    {
        stateMachine.CurPattenCount++;

        FireAround();
        
        if (stateMachine.CurPattenCount < stateMachine.MaxPattenCount[stateMachine.PatternIndex])
        {
            yield return new WaitForSeconds(0.7f);
            stateMachine.ChangeState(stateMachine.FireAroundState);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            stateMachine.ChangeState(stateMachine.ThinkState);
        }
    }

    void FireAround()
    {
        Debug.Log("원 형태로 전체 공격");
        int repeatCount = 50;
        for (int i = 0; i < repeatCount; i++)
        {
            FireAroundShoot(ObjectManager.PoolType.BossBullet4, stateMachine.Boss.transform.position, 2, i, repeatCount);
        }
    }
    
    private void FireAroundShoot(ObjectManager.PoolType bulletPrefab, Vector3 position, int bulletSpeed, int index, int repeatCount)
    {
        GameObject bullet = ObjectManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / repeatCount), Mathf.Sin(Mathf.PI * 2 * index / repeatCount));
        rb.velocity = direction.normalized * bulletSpeed;

        Vector3 rotation = Vector3.forward * (360 * index) / repeatCount + Vector3.forward * 90;
        bullet.transform.Rotate(rotation);
    }
}
