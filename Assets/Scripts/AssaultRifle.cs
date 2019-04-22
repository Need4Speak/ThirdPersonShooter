using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 枪
 * */
public class AssaultRifle : Shooter
{
    //[SerializeField] float rateOfFire = 0.2f;
    public override void Fire()
    {
        base.Fire();

        if(canFire)
        {

        }
    }
}
