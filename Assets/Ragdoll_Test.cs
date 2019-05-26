using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Test : Destructable
{
    public Animator animator;
    private Rigidbody[] bodyParts;
    private MoveController moveController;  // 控制敌人行走

    private void Start()
    {
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
        moveController = GetComponent<MoveController>();
    }

    /**
     * 控制死亡
     * */
    public override void Die()
    {
        base.Die();
        EnableRagdoll(true);
        animator.enabled = false;
    }

    private void Update()
    {
        if (!IsAlive)
        {
            return;
        }
        animator.SetFloat("Vertical", 1);
        moveController.Move(new Vector2(2, 0));
    }

    /**
     * 激活Ragdoll
     * */
    void EnableRagdoll(bool value)
    {
        for(int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }
}
