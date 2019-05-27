using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * 寻路
 * */
[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent Agent;

    [SerializeField] float distanceRemainingThreshold;

    bool m_destionationReached;
    bool destionationReached
    {
        get
        {
            return m_destionationReached;
        }
        set
        {
            m_destionationReached = value;
            if(m_destionationReached)
            {
                if(OnDestionationReached != null)
                {
                    OnDestionationReached();
                }
            }
        }
    }

    public event System.Action OnDestionationReached;  // 是否到达目的地

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    /**
     * 设置目标
     * */
    public void SetTarget(Vector3 target)
    {
        Agent.SetDestination(target);
    }

    private void Update()
    {
        if (destionationReached)
        {
            return;
        }

        if(Agent.remainingDistance < distanceRemainingThreshold)
        {
             destionationReached = true;
        }
    }
}
