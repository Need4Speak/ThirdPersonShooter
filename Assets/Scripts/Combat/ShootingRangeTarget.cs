using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 控制靶
 * */
public class ShootingRangeTarget : Destructable
{
    [SerializeField] float rotationSpeed;  // 倒下速度
    [SerializeField] float repairTime;  // 靶恢复时间

    Quaternion initalRotation;  // 默认
    Quaternion targetRotation;  // 倒地

    bool requiresRotation;  // 是否需要倒地

    private void Awake()
    {
        initalRotation = transform.rotation;
    }

    /**
     * 控制靶倒下
     * */ 
    public override void Die()
    {
        base.Die();
        targetRotation = Quaternion.Euler(transform.right * 90);
        requiresRotation = true;
        GameManager.Instance.Timer.Add(() =>
        {
            targetRotation = initalRotation;
            requiresRotation = true;
        }, repairTime);
    }

    private void Update()
    {
        if (!requiresRotation)
        {
            return;
        }

        transform.rotation =
            Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (transform.rotation == targetRotation)
        {
            requiresRotation = false;
        }
    }

    
}
