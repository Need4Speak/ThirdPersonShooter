using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    /**
     * 控制物体消失
     * */
    public void Despawn(GameObject gameObject, float inSeconds)
    {
        gameObject.SetActive(false);
        GameManager.Instance.Timer.Add(() =>
        {
            gameObject.SetActive(true);
        }, inSeconds);
    }
}
