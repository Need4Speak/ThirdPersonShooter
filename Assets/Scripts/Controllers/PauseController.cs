using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 控制设置面板是否显示
 * */
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
