using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 生命处理
 * */
public class Health : Destructable
{
    public override void Die()
    {
        base.Die();
        print("we died");
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        print("Remaining: " + HitPointsRemaining);
    }
}
