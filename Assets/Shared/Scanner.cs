using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Player> targets;  // 被扫描物体

    Player m_selectedTarget;  // 玩家
    Player selectedTarget
    {
        get
        {
            return m_selectedTarget;
        }

        set
        {
            m_selectedTarget = value;

            if(m_selectedTarget == null)
            {
                return;
            }

            if(OnTargetSelected != null) {
                OnTargetSelected(m_selectedTarget.transform.position);
            }
        }
    }

    public event System.Action<Vector3> OnTargetSelected;

    private void Start()
    {
        rangeTrigger = GetComponent<SphereCollider>();
        targets = new List<Player>();
        PrepareScan();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if(selectedTarget != null)
        {
            Gizmos.DrawLine(transform.position, selectedTarget.transform.position);
        }

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
        if(selectedTarget != null)
        {
            return;
        }

        GameManager.Instance.Timer.Add(ScanForTargets, scanSpeed);
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
    void ScanForTargets()
    {
        print("scan target");
        Collider[] results = Physics.OverlapSphere(transform.position, rangeTrigger.radius);

        for(int i = 0; i < results.Length; i++)
        {
            var player = results[i].transform.GetComponent<Player>();

            if(player == null)
            {
                continue;
            }
            if(!IsInLineOfSight(Vector3.up, player.transform.position))
            {
                continue;
            }
            targets.Add(player);
        }

        if(targets.Count == 1)
        {
            selectedTarget = targets[0];
        } else
        {
            // 检查最近的目标
            float closestTarget = rangeTrigger.radius;
            foreach(var possibleTarget in targets)
            {
                if(Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
                {
                    selectedTarget = possibleTarget;
                }
            }
        }

        PrepareScan();
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
            if(Physics.Raycast(transform.position + eyeHeight, direction.normalized, distanceToTarget, layerMask))
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
