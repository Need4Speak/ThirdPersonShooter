using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataController:MonoBehaviour
{
    public Player player;
    public string filePath = GlobalObjectControl.Instance.filePath;
    private void Awake()
    {
        Debug.Log("保存路径：" + filePath);
    }

    /**
     * 保存游戏，绑定按钮
     * */
    public void SaveGame()
    {
        //SaveByJson();
        SaveByBin();
    }

    /**
     * 加载游戏，绑定按钮
     * */
    public void LoadGame()
    {
        LoadByBin();
    }

    /**
     * 存入json文件中
     * */
    private void SaveByJson()
    {
        PlayerStore playerStore = CreateSaveObj();
        string saveJsonStr = JsonMapper.ToJson(playerStore);
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

            PlayerStore playerStore = JsonMapper.ToObject<PlayerStore>(jsonStr);
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

    private void SetGame(PlayerStore playerStore)
    {
        if(playerStore != null)
        {
            PlayerStore playerStoreGlobal = GlobalObjectControl.Instance.playerStore;
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
        playerStore.Position = player.transform.position;
        playerStore.Rotation = player.transform.rotation;
        playerStore.DamageTaken = player.PlayerHealth.DamageTaken;
        playerStore.HealthAdd = player.PlayerHealth.HealthAdd;
        playerStore.CurrentWeaponIndex = player.PlayerShoot.CurrentWeaponIndex;
        playerStore.RemainingAmmoInClip = player.PlayerShoot.ActiveWeapon.reloader.RoundsRemainingInClip;
        playerStore.RemainingAmmoInInventory = player.PlayerShoot.ActiveWeapon.reloader.RoundsRemainingInInventory;
        return playerStore;
    }
}
