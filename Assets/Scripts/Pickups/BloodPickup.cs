using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 捡到
 * */
public class BloodPickup : PickupItem
{
    private PlayerHealth playerHealth;
    [SerializeField] float respawnTime;  // 刷新间隔
    [SerializeField] float amount;  // 血包血量

    /**
     * 玩家碰到血包后触发事件
     * */
    public override void OnPickup(Transform item)
    {
        playerHealth = item.GetComponent<PlayerHealth>();
        GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerHealth.AddHealth(amount);
    }
}
