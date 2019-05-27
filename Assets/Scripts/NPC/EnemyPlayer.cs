using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(Scanner))]
/**
 * ai
 * */
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    Scanner scanner;

    [SerializeField] Animator animator;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        scanner = GetComponent<Scanner>();
        scanner.OnTargetSelected += ScannerOnTargetSelected;
    }

    /**
     * 设置目标
     * */
    private void ScannerOnTargetSelected(Vector3 position)
    {
        pathFinder.SetTarget(position);
    }

    private void Update()
    {
        animator.SetFloat("Vertical", pathFinder.Agent.velocity.z);
    }
}
