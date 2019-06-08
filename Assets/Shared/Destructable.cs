using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可攻击物体父类
/// </summary>
[RequireComponent(typeof(Collider))]
public class Destructable : MonoBehaviour
{
    [SerializeField] double hitPoints; //总生命值

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;
    public event System.Action OnHealthAdd;

    private double damageTaken; //所受伤害量
    private double healthAdd;  //增加血量

    public double DamageTaken
    {
        get
        {
            return damageTaken;
        }
        set
        {
            damageTaken = value;
        }
    }

    public double HealthAdd
    {
        get
        {
            return healthAdd;
        }
        set
        {
            healthAdd = value;
        }
    }

    public double HitPointsRemaining
    {
        get
        {
            return hitPoints - damageTaken + healthAdd;
            
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
    public virtual void TakeDamage(double amount)
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
     * 初始化生命
     * */
    public void Reset()
    {
        damageTaken = 0;
        healthAdd = 0;
    }

    /**
     * 初始化伤害
     * @param amount: 增加生命值
     * */
    public void AddHealth(double amount)
    {
        healthAdd += amount;

        if (HitPointsRemaining > hitPoints)
        {
            healthAdd = healthAdd - (HitPointsRemaining - hitPoints);
        }

        //Debug.Log("healthAdd: " + healthAdd);
        //Debug.Log("HitPointsRemaining: " + HitPointsRemaining);

        if (OnHealthAdd != null)
        {
            OnHealthAdd();
        }
    }
}
