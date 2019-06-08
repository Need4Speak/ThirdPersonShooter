using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家动画控制
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /**
     * 控制移动动画
     * */
    private void Update()
    {
        animator.SetFloat("Vertical", 
            GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal",
            GameManager.Instance.InputController.Horizontal);
        animator.SetBool("IsWalking",
            GameManager.Instance.InputController.IsWalking);
        //animator.SetBool("IsSprinting",
        //    GameManager.Instance.InputController.IsSprinting);
        animator.SetBool("IsCrouched",
            GameManager.Instance.InputController.IsCrouched);
        animator.SetBool("IsJump",
            GameManager.Instance.InputController.IsJumped);
    }
}
