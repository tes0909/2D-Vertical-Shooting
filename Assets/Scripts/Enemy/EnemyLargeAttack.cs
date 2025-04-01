using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLargeAttack : EnemyAttack
{
    protected override void Shooting()
    {
        if (curShotDelay < maxShotDelay) return;
        
        Shoot(ObjectManager.PoolType.EnemyBullet2, transform.position, Quaternion.identity);
        
        curShotDelay = 0;
    }
}
