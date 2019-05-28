using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : WeaponController
{
    [SerializeField] float shootingSpeed;

    [SerializeField] float burstDurationMax;  // 开火间隔
    [SerializeField] float burstDurationMin;

    EnemyPlayer enemyPlayer;
    bool shouldFire;

    private void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += EnemyPlayerOnTargetSelected;
    }

    /**
     * 发现敌人后
     * */
    private void EnemyPlayerOnTargetSelected(Player target)
    {
        ActiveWeapon.AimTarget = target.transform;
        ActiveWeapon.AimTargetOffset = Vector3.up * 1.5f;
        StartBurst();
    }

    void StartBurst()
    {
        if(!enemyPlayer.EnemyHealth.IsAlive)
        {
            return;
        }
        CheckReload();
        shouldFire = true;

        GameManager.Instance.Timer.Add(EndBurst, UnityEngine.Random.Range(burstDurationMin, burstDurationMax));
    }

    void EndBurst()
    {
        shouldFire = false;
        if(!enemyPlayer.EnemyHealth.IsAlive)
        {
            return;
        }
        CheckReload();
        GameManager.Instance.Timer.Add(StartBurst, shootingSpeed);
    }

    void CheckReload()
    {
        if(ActiveWeapon.reloader.RoundsRemainingInClip == 0)
        {
            ActiveWeapon.Reload();
        }
    }

    private void Update()
    {
        if (!shouldFire || !canFire || !enemyPlayer.EnemyHealth.IsAlive)
            return;
        ActiveWeapon.Fire();
    }
}
