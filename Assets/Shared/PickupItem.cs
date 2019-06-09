using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物体捡拾控制
/// </summary>
public class PickupItem : MonoBehaviour
{
    /// <summary>
    /// 需要按下按键才能拾取
    /// </summary>
    public bool needPressKey = false;
    public GameObject pickupHelp;

    private void Awake()
    {
        //needPressKey = false;
    }

    /// <summary>
    /// 
    /// 玩家接触到拾取物时
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerStay(Collider collider)
    {
        print(collider.tag);
        if (collider.tag != "Player")
        {
            return;
        }

        Debug.Log("in pick up item, needpresskey:" + needPressKey);
        if (needPressKey) {
            pickupHelp.SetActive(true);
            if(GameManager.Instance.InputController.PickupDown)
            {
                PickUp(collider.transform);
            }
        } else
        {
            PickUp(collider.transform);
        }
    }

    /// <summary>
    /// 玩家离开时，关闭提示
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (needPressKey)
        {
            pickupHelp.SetActive(false);
        }
    }


    /// <summary>
    /// 触碰到被捡物体时触发
    /// </summary>
    /// <param name="item">玩家</param>
    public virtual void OnPickup(Transform item)
    {
        print("test");
    }

    void PickUp(Transform item)
    {
        OnPickup(item);
    }
}
