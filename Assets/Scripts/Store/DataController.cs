
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class DataController:MonoBehaviour
{
    public Player player;
    public string filePath;
    private void Awake()
    {
        filePath = GlobalObjectControl.Instance.filePath;
        Debug.Log("保存路径：" + filePath);
    }

    /**
     * 保存游戏，绑定按钮
     * */
    public void SaveGame()
    {
        PlayerStore playerStore = CreateSaveObj();
        //SaveByJson();
        //SaveByBin();
        SaveToRemote(playerStore);
    }

    /// <summary>
    /// 加载游戏，绑定按钮
    /// </summary>
    /// <returns>加载游戏是否成功</returns>
    public bool LoadGame()
    {
        return LoadByRemote();
        //LoadByBin();
        //return true;
    }

    /// <summary>
    /// 存档并上传至服务器
    /// </summary>
    /// <param name="playerStore">要保存的信息</param>
    private void SaveToRemote(PlayerStore playerStore)
    {
        Socket client = GlobalObjectControl.Instance.client;
        Request request = new Request(200, JsonConvert.SerializeObject(playerStore));
        SendMsg(client, JsonConvert.SerializeObject(request));

    }
    /// <summary>
    /// 向远程服务器请求存档
    /// </summary>
    /// <returns>加载游戏是否成功</returns>
    private bool LoadByRemote()
    {
        PlayerStore playerStore = GetStoreFileFromServer();
        if(playerStore != null)
        {
            Debug.Log("服务器存档信息： " + playerStore);
            SetGame(playerStore);
            return true;
        }
        return false;
    }

    /**
     * 存入json文件中
     * */
    private void SaveByJson()
    {
        PlayerStore playerStore = CreateSaveObj();
        string saveJsonStr = JsonConvert.SerializeObject(playerStore);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(saveJsonStr);
        streamWriter.Close();
        Debug.Log("保存成功");
    }

    /**
     * Json 文件中读取存档
     * */
    private void LoadByJson()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            string jsonStr = streamReader.ReadToEnd();
            streamReader.Close();

            PlayerStore playerStore = JsonConvert.DeserializeObject<PlayerStore>(jsonStr);
            SetGame(playerStore);
            Debug.Log("加载成功");
            //UIManager._instance.ShowMessage("");
        }
        else
        {
            Debug.Log("存档文件不存在");
            //UIManager._instance.ShowMessage("存档文件不存在");
        }
    }

    //二进制方法：存档
    private void SaveByBin()
    {
        filePath = GlobalObjectControl.Instance.filePath;
        PlayerStore playerStore = CreateSaveObj();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);
        bf.Serialize(fileStream, playerStore);
        fileStream.Close();

        if (File.Exists(filePath))
        {
            Debug.Log("存档成功");
        } else
        {
            Debug.Log("存档失败 ");
        }
    }

    //二进制方法：读档
    private void LoadByBin()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            PlayerStore playerStore = (PlayerStore)bf.Deserialize(fileStream);
            fileStream.Close();

            SetGame(playerStore);
            Debug.Log("加载成功");
        }
        else
        {
            Debug.Log("存档文件: " + filePath + " 不存在:" );
        }
    }

    /// <summary>
    /// 根据服务器存档设置GlobalObjectControl，供其他脚本调用
    /// </summary>
    /// <param name="playerStore"></param>
    private void SetGame(PlayerStore playerStore)
    {
        if(playerStore != null)
        {
            PlayerStore playerStoreGlobal = GlobalObjectControl.Instance.playerStore;
            playerStoreGlobal.Userid= playerStore.Userid;
            playerStoreGlobal.Position = playerStore.Position;
            playerStoreGlobal.Rotation = playerStore.Rotation;
            playerStoreGlobal.DamageTaken = playerStore.DamageTaken;
            playerStoreGlobal.HealthAdd = playerStore.HealthAdd;
            playerStoreGlobal.CurrentWeaponIndex = playerStore.CurrentWeaponIndex;
            playerStoreGlobal.RemainingAmmoInClip = playerStore.RemainingAmmoInClip;
            playerStoreGlobal.RemainingAmmoInInventory = playerStore.RemainingAmmoInInventory;
            Debug.Log("成功加载配置： " + playerStoreGlobal);
        } else
        {
            Debug.LogError("playerStore = null, 存档加载失败");
        }
        
    }

    /**
     * 初始化要保存的内容
     * */
    private PlayerStore CreateSaveObj()
    {
        PlayerStore playerStore = new PlayerStore();
        playerStore.Userid = GlobalObjectControl.Instance.Userid;
        playerStore.Position = player.transform.position;
        playerStore.Rotation = player.transform.rotation;
        playerStore.DamageTaken = player.PlayerHealth.DamageTaken;
        playerStore.HealthAdd = player.PlayerHealth.HealthAdd;
        playerStore.CurrentWeaponIndex = player.PlayerShoot.CurrentWeaponIndex;
        playerStore.RemainingAmmoInClip = player.PlayerShoot.ActiveWeapon.reloader.RoundsRemainingInClip;
        playerStore.RemainingAmmoInInventory = player.PlayerShoot.ActiveWeapon.reloader.RoundsRemainingInInventory;
        return playerStore;
    }

    /**
     * 尝试连接远程服务器
     * */
    private PlayerStore GetStoreFileFromServer()
    {
        Socket client = GlobalObjectControl.Instance.client;
        //client.Listen(5);

        //Thread t1 = new Thread(sendMsg);
        //t1.Start();
        //Thread t2 = new Thread(ReciveMsg);
        //t2.Start();
        SendMsg(client, JsonConvert.SerializeObject(new Request(1, GlobalObjectControl.Instance.Userid)));
        PlayerStore playerStore = ReciveMsg(client);
        //client.Close();
        //BinaryWriter bw = new BinaryWriter(new FileStream(storeFile,
        //        FileMode.Create));
        //bw.Write(reciveMsg);
        //bw.Close();
        //Debug.Log("reciveMsg: " + reciveMsg);

        return playerStore;
    }
    
    /// <summary>
    /// 从服务器中删除存档文件
    /// </summary>
    public void DeleteStoreFileAtServer()
    {
        Socket client = GlobalObjectControl.Instance.client;
        SendMsg(client, JsonConvert.SerializeObject(new Request(2, GlobalObjectControl.Instance.Userid)));
    }

    /// <summary>
    /// 向特定ip的主机的端口发送数据报
    /// </summary>
    private void SendMsg(Socket client, string msg)
    {
        ServerConfig serverConfig = getServerConfig();
        Debug.Log("server: " + serverConfig.Ip + serverConfig.Port);
        EndPoint point = new IPEndPoint(IPAddress.Parse(serverConfig.Ip), serverConfig.Port);
        client.SendTo(Encoding.UTF8.GetBytes(msg), point);
    }

    /// <summary>
    /// 接收发送给本机ip对应端口号的数据报
    /// </summary>
    private PlayerStore ReciveMsg(Socket client)
    {
        EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
        byte[] buffer = new byte[1024 * 10];
        int length = client.ReceiveFrom(buffer, ref point);//接收数据报
        if(0 != length) {
            string msg = Encoding.UTF8.GetString(buffer);
            PlayerStore playerStore = JsonConvert.DeserializeObject<PlayerStore>(msg);
            Debug.Log("成功接收到PlayerStore: " + playerStore.ToString());
            return playerStore;
        }
        return null;
        //string message = Encoding.UTF8.GetString(buffer, 0, length);
        //Debug.Log(point.ToString() + message);
        //return message;
    }

    /**
     * 产生服务器存储文件
     * */
    private void generateServerConfigFile()
    {
        //string filePath = Application.dataPath + "/StreamingFile/serverConfig.json";
        ServerConfig serverConfig = new ServerConfig("127.0.0.1", 9999);
        JsonUtil.save(serverConfig, GlobalObjectControl.Instance.serverConfigPath);
    }

    /**
     * 获取服务器存储配置
     * */
    private ServerConfig getServerConfig()
    {
        //string filePath = Application.dataPath + "/StreamingFile/serverConfig.json";
        string jsonStr = JsonUtil.LoadByJson(GlobalObjectControl.Instance.serverConfigPath);
        ServerConfig serverConfig = JsonConvert.DeserializeObject< ServerConfig>(jsonStr);
        Debug.Log(serverConfig);
        return serverConfig;
    }
}
