using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 换弹
 * */
public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;

//    int ammo;
    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    private void Awake()
    {
        containerItemId = inventory.Add(this.name, maxAmmo); 
    }

    /**
     * 当前使用弹匣
     * */
    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    /**
     * 判断当前是否正在换弹
     * */
    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    /**
     * 开始换弹
     * */
    public void Reload()
    {
        if(isReloading)
        {
            return;
        }
        isReloading = true;

        int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip);

        print("正在换弹");
        GameManager.Instance.Timer.Add(()=> { ExecuteReload(amountFromInventory);}, reloadTime);
    }

    /**
     * 执行换弹动作
     * */
    private void ExecuteReload(int amount)
    {
        print("完成换弹");
        isReloading = false;

        shotsFiredInClip -= amount;
    }

    /**
     * 弹匣中取弹
     * */
    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
    }

}
