using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/**
 * 保存全局变量
 * */
public class GlobalObjectControl : MonoBehaviour
{
    public bool NewGame;  // 是否为新游戏

    public PlayerStore playerStore;  // 玩家信息保存
    public string filePath;  // 本地存档文件
    public string serverConfigPath;  // 保存远程服务器地址
    public Socket client;  //与服务器的连接
    private string userid;  //登录的用户 id

    public static GlobalObjectControl Instance;

    public string Userid { get => userid; set => userid = value; }

    //初始化
    private void Awake()
    {
        Debug.Log("GlobalObjectControl初始化");
        filePath = Application.dataPath + "/StreamingFile/save.bin";
        serverConfigPath = Application.dataPath + "/StreamingFile/serverConfig.json";
        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000));

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            NewGame = true; // 初始化
        }
        else if (Instance != null)
        {
            client.Close();  // 关闭socket
            Destroy(gameObject);
        }
        //Debug.Log("Instance == null ?: " + Instance == null);
    }

    /**
 * 获取输入
 * */
    private DataController m_DataController;
    public DataController DataController
    {
        get
        {
            if (m_DataController == null)
            {
                m_DataController = new DataController();
            }
            return DataController;
        }
    }
}
