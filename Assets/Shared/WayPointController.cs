using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 设置行动路线点
 * */
public class WayPointController : MonoBehaviour
{
    WayPoint[] wayPoints;

    int currentWayPointIndex = -1;
    public event System.Action<WayPoint> OnWayPointChanged;

    private void Awake()
    {
        wayPoints = GetWayPoints();
    }

    private WayPoint[] GetWayPoints()
    {
        return GetComponentsInChildren<WayPoint>();
    }

    /**
     * 设置下一个行动路线点
     * */
    public void setNextWayPoint()
    {
        currentWayPointIndex++;
        if(currentWayPointIndex == wayPoints.Length)
        {
            currentWayPointIndex = 0;
        }

        if(OnWayPointChanged != null)
        {
            OnWayPointChanged(wayPoints[currentWayPointIndex]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 previousWayPoint = Vector3.zero;  // 记录上一个检查点
        foreach(var waypoint in GetWayPoints())
        {
            Gizmos.DrawSphere(waypoint.transform.position, .2f);

            Vector3 waypointPosition = waypoint.transform.position;
            if (previousWayPoint != Vector3.zero)
            {
                Gizmos.DrawLine(previousWayPoint, waypointPosition);
            }
            previousWayPoint = waypointPosition;
        }
    }
}
