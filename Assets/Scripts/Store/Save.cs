

using UnityEngine;
/**
* 所要保存的信息
* */
[System.Serializable]
class Save
{
    //private Transform m_PlayerTransaform;
    //public Transform PlayerTransaform
    //{
    //    get
    //    {
    //        return m_PlayerTransaform;
    //    }

    //    set
    //    {
    //        m_PlayerTransaform = value;
    //    }
    //}

    //private PlayerHealth m_PlayerHealth;
    //public PlayerHealth PlayerHealth
    //{
    //    get
    //    {
    //        return m_PlayerHealth;
    //    }

    //    set
    //    {
    //        m_PlayerHealth = value;
    //    }
    //}

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
}

