using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人扫描控制
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField]
    float scanSpeed;

    [SerializeField]
    [Range(0, 360)]
    float fieldOfView;

    [SerializeField]
    LayerMask layerMask;

    SphereCollider rangeTrigger;

    /**
     * 扫描范围
     * */
    public float ScanRange
    {
        get
        {
            if (rangeTrigger == null)
            {
                rangeTrigger = GetComponent<SphereCollider>();
            }
            return rangeTrigger.radius;
        }
    }

    public event System.Action OnScanReady;  // 扫描完成后

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,
            transform.position + GetViewAngle(fieldOfView / 2) * GetComponent<SphereCollider>().radius);
        Gizmos.DrawLine(transform.position,
        transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);
    }

    /**
     * 准备扫描
     * 
     * */
    void PrepareScan()
    {
        GameManager.Instance.Timer.Add(()=> {
            if(OnScanReady != null)
            {
                OnScanReady();
            }
        }, scanSpeed);
    }

    /**
     * 获取可视区域
     * */
    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    /**
     * 扫描物体
     * */
    public List<T> ScanForTargets<T>()
    {
        print("scan target");
        List<T> targets = new List<T>();
        Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

        for(int i = 0; i < results.Length; i++)
        {
            var player = results[i].transform.GetComponent<T>();

            if(player == null)
            {
                continue;
            }
            if(!IsInLineOfSight(Vector3.up, results[i].transform.position))
            {
                continue;
            }
            targets.Add(player);
        }

        //if(targets.Count == 1)
        //{
        //    selectedTarget = targets[0];
        //} else
        //{
        //    // 检查最近的目标
        //    float closestTarget = rangeTrigger.radius;
        //    foreach(var possibleTarget in targets)
        //    {
        //        if(Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
        //        {
        //            selectedTarget = possibleTarget;
        //        }
        //    }
        //}

        PrepareScan();
        return targets;
    }

    /**
     * 是否看到物体
     * */
    bool IsInLineOfSight(Vector3 eyeHeight, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        if(Vector3.Angle(transform.forward, direction.normalized) < fieldOfView /2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            // 视野前有物体
            if(Physics.Raycast(transform.position + eyeHeight + transform.forward * 0.3f, direction.normalized, distanceToTarget, layerMask))
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
