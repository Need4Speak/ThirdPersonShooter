using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 捡物体
 * */
public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        print(collider.tag);
        if (collider.tag != "Player")
        {
            return;
        }

        PickUp(collider.transform);
    }

    /**
     * 触碰到被捡物体时触发
     * */
    public virtual void OnPickup(Transform item)
    {
        print("test");
    }

    void PickUp(Transform item)
    {
        OnPickup(item);
    }
}
