using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 捡弹控制
/// </summary>
public class AmmoPickup : PickupItem
{
    [SerializeField] EWeaponType weaponType;  //弹药类型
    [SerializeField] float respawnTime;  // 刷新间隔
    [SerializeField] int amount;

    /**
     * 捡子弹
     * */
    public override void OnPickup(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerInventory.Put(weaponType.ToString(), amount);
        item.GetComponent<Player>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
    }
}

