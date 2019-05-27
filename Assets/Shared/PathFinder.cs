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

    bool m_destinationReached;
    bool destinationReached
    {
        get
        {
            return m_destinationReached;
        }
        set
        {
            m_destinationReached = value;
            if(m_destinationReached)
            {
                if(OnDestionationReached != null)
                {
                    OnDestionationReached();
                }
            }
        }
    }

    public event System.Action OnDestionationReached;  // 是否到达目的地

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    /**
     * 设置目标
     * */
    public void SetTarget(Vector3 target)
    {
        destinationReached = false;
        Agent.SetDestination(target);
    }

    private void Update()
    {
        if (destinationReached)
        {
            return;
        }

        if(Agent.remainingDistance < distanceRemainingThreshold)
        {
             destinationReached = true;
        }
    }
}
