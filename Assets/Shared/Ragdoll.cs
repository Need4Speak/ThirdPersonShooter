using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] bodyParts;  // 组件各个部分

    private void Start()
    {
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);  // 开始禁用rragdoll
    }

    public void EnableRagdoll(bool value)
    {
        animator.enabled = !value;
        for(int i = 0; i < bodyParts.Length;i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }
}
