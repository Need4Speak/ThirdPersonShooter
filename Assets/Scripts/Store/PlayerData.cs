using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player player;
    public string filePath = Application.dataPath + "/StreamingFile/save.json";
    private void Awake()
    {
        Debug.Log("保存路径：" + filePath);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Save save = CreateSaveObj();
        string saveJsonStr = JsonMapper.ToJson(save);
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

            Save save = JsonMapper.ToObject<Save>(jsonStr);
            SetGame(save);
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
        Save save = CreateSaveObj();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);
        bf.Serialize(fileStream, save);
        fileStream.Close();

        if (File.Exists(filePath))
        {
            Debug.Log("保存成功");
        }
    }

    //二进制方法：读档
    private void LoadByBin()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            Save save = (Save)bf.Deserialize(fileStream);
            fileStream.Close();

            SetGame(save);
            Debug.Log("加载成功");
        }
        else
        {
            Debug.Log("存档文件不存在:" + filePath);
        }
    }

    private void SetGame(Save save)
    {
        //player.transform.position = save.PlayerTransaform.position;
        //player.transform.rotation = save.PlayerTransaform.rotation;
        //player.PlayerHealth = save.PlayerHealth;
        Debug.Log("Save:" + save);
    }

    /**
     * 初始化要保存的内容
     * */
    private Save CreateSaveObj()
    {
        Save save = new Save();
        save.PlayerPosition = player.transform.position;
        //save.PlayerHealth = player.PlayerHealth;
        save.HealthAdd = player.PlayerHealth.HealthAdd;
        save.DamageTaken = player.PlayerHealth.DamageTaken;

        return save;
    }
}
