using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    /**
     * 人物移动
     * */
    public void Move(Vector2 direction)
    {
        transform.position += transform.forward * direction.x * Time.deltaTime +
            transform.right * direction.y * Time.deltaTime;
    }
}
