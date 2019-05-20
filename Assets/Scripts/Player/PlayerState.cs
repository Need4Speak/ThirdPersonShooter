using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 角色相关状态
 * */
public class PlayerState : MonoBehaviour
{
    // 角色移动状态
   public enum EMoveState
    {
        WALKING,
        RUNNING,
        CROUCHING,
        JUMPING
    }

    // 武器状态
    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState MoveState;
    public EWeaponState WeaponState;

    private InputController m_InputController;
    public InputController InputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = GameManager.Instance.InputController;
            }
            return m_InputController;
        }
    }

    private void Update()
    {
        SetMoveState();
        SetWeaponState();
    }

    /**
     * 设置移动状态
     * */
    void SetMoveState()
    {
        MoveState = EMoveState.RUNNING;

        if(InputController.IsWalking)
        {
            MoveState = EMoveState.WALKING;
        } else if (InputController.IsCrouched)
        {
            MoveState = EMoveState.CROUCHING;
        } else if (InputController.IsJumped)
        {
            MoveState = EMoveState.JUMPING;
        }
    }

    /**
    * 设置武器状态
    * */
    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDLE;

        if (InputController.Fire1)
        {
            WeaponState = EWeaponState.FIRING;
        }
        if (InputController.Fire2)
        {
            WeaponState = EWeaponState.AIMING;
        }
        if (InputController.Fire1&& InputController.Fire2)
        {
            WeaponState = EWeaponState.AIMEDFIRING;
        }
    }
}
