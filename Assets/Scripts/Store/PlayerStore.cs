

using UnityEngine;
/**
* Player 所要保存的信息
* */
[System.Serializable]
public class PlayerStore
{
    private SerializableVector3 m_Position;  // 坐标
    public SerializableVector3 Position { get => m_Position; set => m_Position = value; }

    private SerializableQuaternion m_Rotation;  // 方向
    public SerializableQuaternion Rotation { get => m_Rotation; set => m_Rotation = value; }

    private double damageTaken; //所受伤害量
    public double DamageTaken { get => damageTaken; set => damageTaken = value; }

    private double healthAdd;  //增加血量
    public double HealthAdd { get => healthAdd; set => healthAdd = value; }

    public PlayerStore()
    {
        this.m_Position = new SerializableVector3();
        this.m_Rotation = new SerializableQuaternion();
        this.damageTaken = 0.0;
        this.healthAdd = 0.0;
    }

    public PlayerStore(SerializableVector3 m_Position, SerializableQuaternion m_Rotation, double damageTaken, double healthAdd)
    {
        this.m_Position = m_Position;
        this.m_Rotation = m_Rotation;
        this.damageTaken = damageTaken;
        this.healthAdd = healthAdd;
    }

    public override string ToString()
    {
        return Position.ToString() + "" + Rotation.ToString() + "DamageTaken:" + DamageTaken + "HealthAdd:" + HealthAdd;
    }
}

