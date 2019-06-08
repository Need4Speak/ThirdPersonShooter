using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 生命控制
 * */
public class Health : Destructable
{
    [SerializeField] float inSeconds;

    /**
     * 死亡处理
     * */
    public override void Die()
    {
        base.Die();
        GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
        print("we died");
    }

    /**
     * 物体重生
     * */
    private void OnEnable()
    {
        Reset();
    }

    /**
     * 受伤处理
     * */
    public override void TakeDamage(double amount)
    {
        base.TakeDamage(amount);
        print("Remaining: " + HitPointsRemaining);
    }
}
