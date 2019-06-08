using LitJson;
using System;
using System.IO;
using UnityEngine;

public class JsonUtil
{
    /**
     * 将 saveObj 序列化到 filePath 文件中
     * */
    public static void save(object saveObj, string filePath)
    {
        string saveJsonStr = JsonMapper.ToJson(saveObj);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(saveJsonStr);
        streamWriter.Close();
        Debug.Log("保存成功");
    }


    /**
     * 从 filePath 文件中获取 obj 信息
     * */
    public static string LoadByJson( string filePath)
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            string jsonStr = streamReader.ReadToEnd();
            streamReader.Close();
            return jsonStr;
            Debug.Log("加载成功");
        }
        else
        {
            return null;
            Debug.Log("存档文件不存在");
            //UIManager._instance.ShowMessage("存档文件不存在");
        }
    }
}
