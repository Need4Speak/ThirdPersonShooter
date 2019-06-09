using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器拾取
/// </summary>
public class WeaponPickup : PickupItem
{
    private PlayerHealth playerHealth;
    [SerializeField] float respawnTime;  // 刷新间隔

    /**
     * 玩家碰到血包后触发事件
     * */
    public override void OnPickup(Transform item)
    {
        Debug.Log("拾取武器");
        //playerHealth = item.GetComponent<PlayerHealth>();
        //GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        //playerHealth.AddHealth(amount);
    }
}
