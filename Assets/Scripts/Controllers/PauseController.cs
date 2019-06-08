using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏暂停与继续控制
/// </summary>
public class PauseController : MonoBehaviour
{
    private bool paused;

    public bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }


    private void Awake()
    {
        paused = false;
    }

    /**
     * 开始游戏
     * */
    public void StartGame()
    {
        Time.timeScale = 1;
        //GameObject.Find("Player").GetComponent<Player>().gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
