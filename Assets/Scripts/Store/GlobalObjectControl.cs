using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 保存全局变量
 * */
public class GlobalObjectControl : MonoBehaviour
{
    public PlayerStore playerStore;
    public string filePath;

    public static GlobalObjectControl Instance;
    //初始化
    private void Awake()
    {
        Debug.Log("GlobalObjectControl初始化");
        filePath = Application.dataPath + "/StreamingFile/save.json";
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        Debug.Log("Instance == null ?: " + Instance == null);
    }

    //public PlayerStore PlayerStore { get => playerStore; set => playerStore = value; }
}
