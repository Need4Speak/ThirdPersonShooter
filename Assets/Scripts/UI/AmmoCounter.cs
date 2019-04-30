using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text text;
    PlayerShoot playerShoot;
    WeaponReloader reloader;

    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    /**
     * 玩家加入时进行初始化
     * */
    void HandleOnLocalPlayerJoined(Player player)
    {
        //inventory = player.GetComponent<Container>();
        playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;

        //reloader = playerShoot.ActiveWeapon.reloader;
        //reloader.OnAmmoChanged += HandleAmmoChanged;
    }

    private void HandleOnWeaponSwitch(Shooter activeWeapon)
    {
        reloader = activeWeapon.reloader;
        reloader.OnAmmoChanged += HandleAmmoChanged;
        HandleAmmoChanged();
    }

    private void HandleAmmoChanged()
    {
        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInClip = reloader.RoundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
