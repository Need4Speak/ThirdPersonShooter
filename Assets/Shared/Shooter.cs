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
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioFire; //开火音效
    [SerializeField] AudioController audioReload; //换弹音效

    Transform muzzle; // 枪口

    public WeaponReloader reloader;

    private ParticleSystem muzzleFireParticleSystem;  // 枪口火焰

    float nextFireAllowed; //射击时间间隔
    public bool canFire; // 是否可以射击、

    public Transform AimTarget;
    public Vector3 AimTargetOffset;

    /**
     * 装备武器
     * */
    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
        //transform.SetParent(hand);
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
        audioReload.Play();
    }

    /**
     * 开火效果
     * */
    void FireEffect()
    {
        if(muzzleFireParticleSystem == null)
        {
            return;
        }
        print("muzzleFireParticleSystem.Play()");
        muzzleFireParticleSystem.Play();
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

        muzzle.LookAt(AimTarget.position + AimTargetOffset);  // 枪口指向瞄准物

        FireEffect();

        // 实例化子弹
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        canFire = true;
        audioFire.Play();
    }

}
