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

    Shooter[] weapons;

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
}
