using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallAttack : EnemyAttack
{
    protected override void Shooting()
    {
        if (curShotDelay < maxShotDelay) return;
        
        Shoot(ObjectManager.PoolType.EnemyBullet1, transform.position + new Vector3(-0.4f, 0.2f), Quaternion.identity);
        Shoot(ObjectManager.PoolType.EnemyBullet1, transform.position + new Vector3(0.4f, 0.2f), Quaternion.identity);

        curShotDelay = 0;
    }
}
