using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController
{
    bool IsPlayerAlive;
    private void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().PlayerHealth.OnDeath += PlayerHealthOnDeath;
    }

    /**
     * 玩家时候
     * */
    private void PlayerHealthOnDeath()
    {
        IsPlayerAlive = false;
    }

    private void Update()
    {
        if (!IsPlayerAlive)
        {
            return;
        }

        if(GameManager.Instance.InputController.Fire1 && canFire)
        {
            ActiveWeapon.Fire();
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
