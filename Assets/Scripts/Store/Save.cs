

using UnityEngine;
/**
* 所要保存的信息
* */
[System.Serializable]
class Save
{
    private SerializableVector3 m_PlayerPosition;
    public SerializableVector3 PlayerPosition { get; set; }

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
    public double DamageTaken { get; set; }

    private double healthAdd;  //增加血量
    public double HealthAdd { get; set; }

    public override string ToString()
    {
        return PlayerPosition.ToString() + "DamageTaken:" + DamageTaken + "HealthAdd:" + HealthAdd;
    }
}

