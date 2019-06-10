using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePickupScript : MonoBehaviour
{
    public Shooter[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        DisablePickupScripts();
    }

    // Update is called once per frame
    void Update()
    {
        DisablePickupScripts();
    }

    /// <summary>
    /// 关闭手持武器的可拾取脚本
    /// </summary>
    private void DisablePickupScripts()
    {
        Debug.Log("weapon = null: " + (weapons == null));
        for (int i = 0; i < weapons.Length; i++)
        {
            Debug.Log("weapon  = null: " + (weapons[i] == null));
            Debug.Log("weapon pickup = null: " + (weapons[i].GetComponentInChildren<WeaponPickup>() == null));
            //weapons[i].GetComponent<WeaponPickup>().enabled = false;
        }
    }
}
