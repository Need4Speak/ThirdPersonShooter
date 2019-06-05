using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**
 * 保存全局变量
 * */
public class GlobalObjectControl : MonoBehaviour
{
    public PlayerStore playerStore;
    public string filePath;
    public bool NewGame;

    public static GlobalObjectControl Instance;
    //初始化
    private void Awake()
    {
        Debug.Log("GlobalObjectControl初始化");
        filePath = Application.dataPath + "/StreamingFile/save.txt";
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            // 通过存档文件是否存在判断是否为新游戏
            if(File.Exists(filePath))
            {
                NewGame = false;
            } else
            {
                NewGame = true;
            }
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        Debug.Log("Instance == null ?: " + Instance == null);
    }

    //public PlayerStore PlayerStore { get => playerStore; set => playerStore = value; }
}
