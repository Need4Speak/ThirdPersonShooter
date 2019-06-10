using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器控制
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] float weaponSwitchTime;

    [HideInInspector] public bool canFire;

    private Shooter[] weapons;

    private int currentWeaponIndex;  // 当前武器索引
    public int CurrentWeaponIndex { get => currentWeaponIndex; set => currentWeaponIndex = value; }

    Transform weaponHolster;

    public event System.Action<Shooter> OnWeaponSwitch;

    Shooter m_ActiveWeapon;
    public Shooter ActiveWeapon
    {
        get
        {
            return m_ActiveWeapon;
        }
    }

    /**
     * 初始化第一把枪
     * */
    private void Awake()
    {
        canFire = true;
        weaponHolster = transform.Find("Weapons");
        weapons = weaponHolster.GetComponentsInChildren<Shooter>(true);
        //print(weapons.Length);
        if (weapons.Length > 0)
        {
            Equip(0);
            //ActiveWeapon.GetComponent<WeaponPickup>().enabled = false;  //关闭被捡拾脚本
        }
    }

    /**
     * 初所有武器设为不可用
     * */
    void DeactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolster);
        }
    }

    /**
     * 前后切枪
     * */
    internal void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex += direction;
        if (currentWeaponIndex > weapons.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        print("currentWeaponIndex: " + currentWeaponIndex);
        GameManager.Instance.Timer.Add(() => { Equip(currentWeaponIndex); },
            weaponSwitchTime);

    }

    /**
     * 装备切换后的枪
     * */
    internal void Equip(int index)
    {
        DeactivateWeapons();
        canFire = true;
        //print("index: " + index);
        m_ActiveWeapon = weapons[index];
        m_ActiveWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        if (OnWeaponSwitch != null)
        {
            OnWeaponSwitch(m_ActiveWeapon);
        }
    }

    /// <summary>
    /// 更换武器
    /// </summary>
    /// <param name="index">要更换武器的索引</param>
    /// <param name="newWeapon">新武器</param>
    public void ChangeWeapon(int index, Shooter newWeapon)
    {
        weapons[index] = newWeapon;
        Equip(index);
    }

    /// <summary>
    /// 根据索引获取武器
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>武器引用</returns>
    public Shooter GetWeaponByIndex(int index)
    {
        if(weapons.Length > 0)
        {
            return weapons[index];
        }
        return null;
    }

    /// <summary>
    /// 获取当前所持有的武器
    /// </summary>
    /// <returns>武器</returns>
    public Shooter GetCurrentWeapon()
    {
        return GetWeaponByIndex(currentWeaponIndex);
    }
}
