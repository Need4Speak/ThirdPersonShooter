using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 子弹
 * */
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            CheckDestructable(hit.transform);
        }
    }
    /// <summary>
    /// 子弹杀伤物体
    /// </summary>
    /// <param name="other">被子弹打到的物体</param>
    void CheckDestructable(Transform other)
    {
        print("Hit: " + other.name);
        var destructable = other.GetComponent<Destructable>();
        if (destructable == null)
            return;
        destructable.TakeDamage(damage);
        Destroy(gameObject);
    }

}
