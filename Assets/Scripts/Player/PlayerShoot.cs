using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController
{
    [SerializeField] PauseController pauseController;
    private bool IsPlayerAlive;
    private void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().PlayerHealth.OnDeath += PlayerHealthOnDeath;
    }

    private void PlayerHealthOnDeath()
    {
        IsPlayerAlive = false;
    }

    /**
     * 玩家状态
     * */
    private void Update()
    {
        if (pauseController.Paused || !IsPlayerAlive)
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
