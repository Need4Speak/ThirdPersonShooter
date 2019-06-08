using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画控制
/// </summary>
[RequireComponent(typeof(PathFinder))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    Vector3 lastPosition;

    PathFinder pathFinder;

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
    }

    private void Update()
    {
        float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("IsWalking", true);
        animator.SetFloat("Vertical", velocity / pathFinder.Agent.speed);
    }
}
