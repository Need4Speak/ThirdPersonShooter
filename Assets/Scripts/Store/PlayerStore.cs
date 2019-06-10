using System.Text;
using UnityEngine;
/**
* Player 所要保存的信息
* */
[System.Serializable]
public class PlayerStore
{
    private string userid;  // 用户账号
    public string Userid { get => userid; set => userid = value; }

    private SerializableVector3 m_Position;  // 坐标
    public SerializableVector3 Position { get => m_Position; set => m_Position = value; }

    private SerializableQuaternion m_Rotation;  // 方向
    public SerializableQuaternion Rotation { get => m_Rotation; set => m_Rotation = value; }

    private double damageTaken; //所受伤害量
    public double DamageTaken { get => damageTaken; set => damageTaken = value; }

    private double healthAdd;  //增加血量
    public double HealthAdd { get => healthAdd; set => healthAdd = value; }

    private int currentWeaponIndex;  // 当前武器索引
    public int CurrentWeaponIndex { get => currentWeaponIndex; set => currentWeaponIndex = value; }

    private int remainingAmmoInClip;  // 弹匣中剩余弹药量
    public int RemainingAmmoInClip { get => remainingAmmoInClip; set => remainingAmmoInClip = value; }

    private int remainingAmmoInInventory;  // 总剩余弹药量
    public int RemainingAmmoInInventory { get => remainingAmmoInInventory; set => remainingAmmoInInventory = value; }

    public PlayerStore()
    {
        this.userid = "empty";
        this.m_Position = new SerializableVector3();
        this.m_Rotation = new SerializableQuaternion();
        this.damageTaken = 0.0;
        this.healthAdd = 0.0;
        this.currentWeaponIndex = 0;
        this.remainingAmmoInClip = 0;
        this.remainingAmmoInInventory = 0;
    }

    public PlayerStore(SerializableVector3 m_Position, SerializableQuaternion m_Rotation, 
        double damageTaken, double healthAdd, int currentWeaponIndex, int remainingAmmoInClip, int remainingAmmoInInventory)
    {
        this.m_Position = m_Position;
        this.m_Rotation = m_Rotation;
        this.damageTaken = damageTaken;
        this.healthAdd = healthAdd;
        this.currentWeaponIndex = currentWeaponIndex;
        this.remainingAmmoInClip = remainingAmmoInClip;
        this.remainingAmmoInInventory = remainingAmmoInClip;
    }

    public PlayerStore(string userid, SerializableVector3 m_Position, SerializableQuaternion m_Rotation,
    double damageTaken, double healthAdd, int currentWeaponIndex, int remainingAmmoInClip, int remainingAmmoInInventory)
    {
        this.userid = userid;
        this.m_Position = m_Position;
        this.m_Rotation = m_Rotation;
        this.damageTaken = damageTaken;
        this.healthAdd = healthAdd;
        this.currentWeaponIndex = currentWeaponIndex;
        this.remainingAmmoInClip = remainingAmmoInClip;
        this.remainingAmmoInInventory = remainingAmmoInClip;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("user id: " + userid).Append(", ").Append(Position.ToString()).Append(", ").Append(Rotation.ToString()).Append(", ").Append("DamageTaken:" + DamageTaken).Append(", ").
            Append("HealthAdd:" + HealthAdd).Append("currentWeaponIndex: " + currentWeaponIndex).Append(", ").
            Append("remainingAmmoInClip: " + remainingAmmoInClip).Append(", ").
            Append("remainingAmmoInInventory: " + remainingAmmoInInventory);
        return sb.ToString();
    }
}

