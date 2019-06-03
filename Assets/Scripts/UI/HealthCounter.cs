using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 统计玩家生命
 * */
public class HealthCounter : MonoBehaviour
{
    [SerializeField] Text text;
    private PlayerHealth playerHealth;
    private double health;

    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    /**
     * 玩家加入时进行初始化
     * */
    void HandleOnLocalPlayerJoined(Player player)
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.OnDamageReceived += HandleOnHealthChanged;
        playerHealth.OnHealthAdd += HandleOnHealthChanged;
        HandleOnHealthChanged();
    }

    private void HandleOnHealthChanged()
    {
        health = playerHealth.HitPointsRemaining;
        Debug.Log("HitPointsRemaining: " + playerHealth.HitPointsRemaining);
        text.text = string.Format("{0}", health);
    }
}
