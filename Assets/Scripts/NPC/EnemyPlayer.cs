using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人控制
/// </summary>
[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
//[RequireComponent(typeof(Scanner))]
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    [SerializeField] Scanner playerScanner;

    [SerializeField] Soldier EnemySetting;

    public event System.Action<Player> OnTargetSelected;

    Player priorityTarget;
    List<Player> myTargets;

    private EnemyHealth m_EnemyHealth;
    public EnemyHealth EnemyHealth
    {
        get
        {
            if(m_EnemyHealth == null)
            {
                m_EnemyHealth = GetComponent<EnemyHealth>();
            }
            return m_EnemyHealth;
        }
    }

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.Agent.speed = EnemySetting.walkSpeed;
        playerScanner.OnScanReady += ScannerOnScanReady;
        ScannerOnScanReady();

        EnemyHealth.OnDeath += EnemyHealthOnDeath;

    }

    private void EnemyHealthOnDeath()
    {
         
    }

    /**
     * 执行目标扫描
     * */
    private void ScannerOnScanReady()
    {
        if(priorityTarget != null)
        {
            return;
        }
        myTargets = playerScanner.ScanForTargets<Player>();

        if(myTargets.Count == 1)
        {
            priorityTarget = myTargets[0];
        } else
        {
            SelectClosetTarget();
        }

        if(priorityTarget != null)
        {
            if(OnTargetSelected != null)
            {
                OnTargetSelected(priorityTarget);
            }

            SetDestinationToPriorityTarget();
        }
    }

    /**
     * 设置目标
     * */
    private void SetDestinationToPriorityTarget()
    {
        pathFinder.SetTarget(priorityTarget.transform.position);
    }

    /**
     * 选择最近的目标
     * */
    private void SelectClosetTarget()
    {
        float cloestTarget = playerScanner.ScanRange;
        foreach(var possibleTarget in myTargets)
        {
            if(Vector3.Distance(transform.position, possibleTarget.transform.position) < cloestTarget)
            {
                priorityTarget = possibleTarget;
            }
        }
    }

    /**
     * 跟随目标
     * */
    private void Update()
    {
        if(priorityTarget == null)
        {
            return;
        }

        transform.LookAt(priorityTarget.transform.transform.position);
    }

}
