using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人生命控制
/// </summary>
public class EnemyHealth : Destructable
{
    [SerializeField]
    Ragdoll ragdoll;

    public EnemyShoot enemyShoot;

    public override void Die()
    {
        base.Die();
        ragdoll.EnableRagdoll(true);

    }
}
