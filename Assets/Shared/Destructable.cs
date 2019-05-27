using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Destructable : MonoBehaviour
{
    [SerializeField] float hitPoints; //

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken; //所受伤害量

    public float HitPointsRemaining
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }

    /**
     * 判断物体是否存活
     * */
    public bool IsAlive
    {
        get
        {
            return HitPointsRemaining > 0;
        }
    }

    public virtual void Die()
    {
        //if (!IsAlive)
        //{
        //    return;
        //}
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    /**
     * 伤害计算
     * */
    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;
        if(OnDamageReceived != null)
        {
            OnDamageReceived();
        }

        if(HitPointsRemaining <= 0)
        {
            Die();
        }
    }

    /**
     * 初始化伤害
     * */
    public void Reset()
    {
        damageTaken = 0;
    }
}
