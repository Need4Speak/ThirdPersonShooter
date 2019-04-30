using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float weaponSwitchTime;

    Shooter[] weapons;
    Shooter activeWeapon;

    int currentWeaponIndex;
    bool canFire;
    Transform weaponHolster;

    public event System.Action<Shooter> OnWeaponSwitch;

    public Shooter ActiveWeapon
    {
        get
        {
            return activeWeapon;
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
        print(weapons.Length);
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
    void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex += direction;
        if(currentWeaponIndex > weapons.Length - 1)
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
    void Equip(int index)
    {
        DeactivateWeapons();
        canFire = true;
        //print("index: " + index);
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        if(OnWeaponSwitch != null)
        {
            OnWeaponSwitch(activeWeapon);
        }
    }

    private void Update()
    {
        if(GameManager.Instance.InputController.Fire1 && canFire)
        {
            activeWeapon.Fire();
        }
        if (GameManager.Instance.InputController.MouseWheelUp)
        {
            SwitchWeapon(1);
        }
        if (GameManager.Instance.InputController.MouseWheelDown)
        {
            SwitchWeapon(-1);
        }
    }
}
