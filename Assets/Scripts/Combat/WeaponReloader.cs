using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 换弹控制
 * */
public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo; //最大弹药量
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;  // 弹匣弹药量
    [SerializeField] Container inventory; //总弹药量
    [SerializeField] EWeaponType weaponType;

    int ammo;
    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public event System.Action OnAmmoChanged;  //弹药数量变化时触发事件

    /**
     * 当前弹匣子弹数
     * */
    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    /**
     * 当前所有子弹数
     * */
    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
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

    void Awake()
    {
        inventory.OnContainerReady += () => {
            containerItemId = inventory.Add(weaponType.ToString(), maxAmmo);
        };
        Debug.Log("weaponType.ToString(): " + weaponType.ToString() + "containerItemId: " + containerItemId);
    }

    /**
     * 开始换弹
     * */
    public void Reload()
    {
        if (isReloading)
        {
            return;
        }
        isReloading = true;
        int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip);
        print("正在换弹");
        GameManager.Instance.Timer.Add(() => { ExecuteReload(amountFromInventory); }, reloadTime);
    }

    /**
     * 执行换弹动作
     * */
    private void ExecuteReload(int amount)
    {
        print("完成换弹");
        isReloading = false;
        shotsFiredInClip -= amount;

        if(OnAmmoChanged != null)
        {
            OnAmmoChanged();
        }
    }

    /**
     * 弹匣中取弹
     * */
    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged();
    }

    /**
     * 子弹数量变化时触发
     * */
    public void HandleOnAmmoChanged()
    {
        if(OnAmmoChanged != null)
        {
            OnAmmoChanged();
        }
    }

}
