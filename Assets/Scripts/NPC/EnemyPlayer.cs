using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
//[RequireComponent(typeof(Scanner))]
/**
 * ai
 * */
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    [SerializeField] Scanner playerScanner;

    Player priorityTarget;
    List<Player> myTargets;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        playerScanner.OnScanReady += ScannerOnScanReady;
        ScannerOnScanReady();
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

}
