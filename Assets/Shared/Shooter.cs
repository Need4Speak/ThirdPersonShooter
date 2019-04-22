using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 人
 * */
public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    
    [HideInInspector] public Transform muzzle; // 枪口

    private WeaponReloader reloader;

    float nextFireAllowed; //射击时间间隔
    public bool canFire; // 是否可以射击、

    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
    }

    /**
     * 换弹
     * */
    public void Reload()
    {
        if(reloader == null)
        {
            return;
        }
        reloader.Reload();
    }

    /**
     * 判断是否可以射击
     * */
    public virtual void Fire()
    {
        canFire = false;

        if(Time.time < nextFireAllowed)
        {
            return;
        }

        // 判断是否可以发射子弹
        if(reloader != null)
        {
            if(reloader.IsReloading)
            {
                return;
            }
            if(reloader.RoundsRemainingInClip == 0)
            {
                return;
            }

            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;
        //print("Firing in " + Time.time);

        // 实例化子弹
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        canFire = true;
    }

}
