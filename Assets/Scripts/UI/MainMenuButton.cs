using LitJson;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 开始页面按钮事件监听
 * */
public class MainMenuButton : MonoBehaviour
{
    private Socket client;

    private string serverConfigPath;
    private string storeFile;

    public GameObject LoadFailMsg;
    private DataController dataController;

    public void Awake()
    {
        dataController = new DataController();
        serverConfigPath = Application.dataPath + "/StreamingFile/serverConfig.json";
        storeFile = Application.dataPath + "/StreamingFile/save2.txt";
        //client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000));

        LoadFailMsg.SetActive(false);
    }
    /**
     * 开始新游戏
     * */
    public void PlayGame()
    {
        // 删除存档文件，开始新游戏
        dataController.DeleteStoreFileAtServer();
        GlobalObjectControl.Instance.NewGame = true;
        SceneManager.LoadScene("Scenes/Game");
    }

    /**
     * 读取游戏
     * */
    public void LoadGame()
    {
        //DataController dataController = new DataController();
        bool result = dataController.LoadGame();
        if(result)
        {
            GlobalObjectControl.Instance.NewGame = false;
            SceneManager.LoadScene("Scenes/Game");
        } else
        {
            ActiveLoadFailMsg();
        }

    }

    /// <summary>
    /// 显示加载失败窗口
    /// </summary>
    private void ActiveLoadFailMsg()
    {
        LoadFailMsg.SetActive(true);
        GameManager.Instance.Timer.Add(() => { LoadFailMsg.SetActive(false); }, 2); // 弹出对话框，2两秒后消失
    }

    /**
     * 退出游戏
     * */
    public void QuitGame()
    {
        Application.Quit();
    }

    public void testButton()
    {
        //connectToServer();

        TestJson();
    }

    private void TestJson()
    {
        PlayerStore playerStore = new PlayerStore();
        string output = JsonConvert.SerializeObject(playerStore);
        Debug.Log("json str: " + output);
        PlayerStore deserializedPlayerStore = JsonConvert.DeserializeObject<PlayerStore>(output);
        Debug.Log("json obj: " + deserializedPlayerStore);
    }


}
