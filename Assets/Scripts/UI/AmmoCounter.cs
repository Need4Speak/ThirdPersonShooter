using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text text;
    PlayerShoot playerShoot;

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
        playerShoot.ActiveWeapon.reloader.OnAmmoChanged += OnHandleAmmoChanged;
    }

    private void OnHandleAmmoChanged()
    {
        text.text = playerShoot.ActiveWeapon.reloader.RoundsRemainingInClip.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
