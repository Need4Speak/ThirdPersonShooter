using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制敌人延检查点移动
/// </summary>
[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] WayPointController wayPointController;

    [SerializeField] float waitTimeMin;  // 到达检查点停留时间
    [SerializeField] float waitTimeMax;

    PathFinder pathFinder;

    private EnemyPlayer m_EnemyPlayer;
    public EnemyPlayer EnemyPlayer
    {
        get
        {
            if (m_EnemyPlayer == null)
            {
                m_EnemyPlayer = GetComponent<EnemyPlayer>();
            }
            return m_EnemyPlayer;
        }
    }

    private void Start()
    {
        wayPointController.setNextWayPoint();
    }

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        EnemyPlayer.EnemyHealth.OnDeath += EnemyHealthOnDeath;
        EnemyPlayer.OnTargetSelected += EnemyPlayerOnTargetSelected;

    }

    private void OnEnable()
    {
        pathFinder.OnDestionationReached += PathFinderOnDestinationReached;
        wayPointController.OnWayPointChanged += WayPointControllerOnWayPointChanged;
    }

    /**
     * 锁定目标后
     * */
    private void EnemyPlayerOnTargetSelected(Player obj)
    {
        pathFinder.Agent.Stop();
    }

    private void EnemyHealthOnDeath()
    {
        pathFinder.Agent.Stop();
    }

    /**
     * 设置下一个检查点
     * */
    private void WayPointControllerOnWayPointChanged(WayPoint wayPoint)
    {
        pathFinder.SetTarget(wayPoint.transform.position);
    }

    /**
     * 敌人到达检查点后动作
     * */
    private void PathFinderOnDestinationReached()
    {
        GameManager.Instance.Timer.Add(wayPointController.setNextWayPoint, UnityEngine.Random.Range(waitTimeMin, waitTimeMax));
    }
}
