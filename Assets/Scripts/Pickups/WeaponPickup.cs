using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器拾取
/// </summary>
public class WeaponPickup : PickupItem
{
    private PlayerShoot playerShoot;
    public GameObject weapons;
    //[SerializeField] float respawnTime;  // 刷新间隔

    /**
     * 玩家碰到枪后触发事件
     * */
    public override void OnPickup(Transform player)
    {
        playerShoot = player.GetComponent<PlayerShoot>();
        int currentWeaponIndex = playerShoot.CurrentWeaponIndex;
        Shooter currentWeapon = playerShoot.GetCurrentWeapon();
        Shooter pickedWeapon = gameObject.GetComponent<Shooter>();

        
        pickedWeapon.GetComponent<WeaponPickup>().enabled = false;  //关闭被捡拾脚本

        currentWeapon.transform.position = pickedWeapon.transform.position;  // 丢弃已持有的武器
        currentWeapon.transform.rotation = pickedWeapon.transform.rotation;
        currentWeapon.transform.parent = weapons.transform;  // 解除被丢弃武器与手的父子关系
        currentWeapon.GetComponent<WeaponPickup>().enabled = true;  //开启被捡拾脚本


        playerShoot.ChangeWeapon(currentWeaponIndex, pickedWeapon);
        

        Debug.Log("拾取武器");
        //playerHealth = item.GetComponent<PlayerHealth>();
        //GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        //playerHealth.AddHealth(amount);
    }
}
