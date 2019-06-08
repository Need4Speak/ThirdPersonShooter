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
    public PlayerStore playerStore;
    public string filePath;
    public bool NewGame;

    public string serverConfigPath;  // 保存远程服务器地址
    public string storeFile;  //存档文件
    public Socket client;

    public static GlobalObjectControl Instance;
    //初始化
    private void Awake()
    {
        Debug.Log("GlobalObjectControl初始化");
        filePath = Application.dataPath + "/StreamingFile/save.txt";
        serverConfigPath = Application.dataPath + "/StreamingFile/serverConfig.json";
        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000));
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            NewGame = true; // 初始化
            //// 通过存档文件是否存在判断是否为新游戏
            //if(File.Exists(filePath))
            //{
            //    NewGame = false;
            //} else
            //{
            //    NewGame = true;
            //}
        }
        else if (Instance != null)
        {
            client.Close();  // 关闭socket
            Destroy(gameObject);
        }

        Debug.Log("Instance == null ?: " + Instance == null);
    }
    //public PlayerStore PlayerStore { get => playerStore; set => playerStore = value; }
}
