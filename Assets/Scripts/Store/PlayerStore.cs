

using UnityEngine;
/**
* Player 所要保存的信息
* */
[System.Serializable]
public class PlayerStore
{
    private SerializableVector3 m__Position;  // 坐标
    public SerializableVector3 Position { get; set; }

    private double damageTaken; //所受伤害量
    public double DamageTaken { get; set; }

    private double healthAdd;  //增加血量
    public double HealthAdd { get; set; }

    public override string ToString()
    {
        return Position.ToString() + "DamageTaken:" + DamageTaken + "HealthAdd:" + HealthAdd;
    }
}

