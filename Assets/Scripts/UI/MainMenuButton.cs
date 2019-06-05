using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 开始页面按钮事件监听
 * */
public class MainMenuButton : MonoBehaviour
{
    /**
     * 开始新游戏
     * */
    public void PlayGame()
    {
        string filePath = GlobalObjectControl.Instance.filePath;
        // 删除存档文件，开始新游戏
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            GlobalObjectControl.Instance.NewGame = true;
        }

        SceneManager.LoadScene("Scenes/Game");
    }

    /**
     * 读取游戏
     * */
    public void LoadGame()
    {
        DataController dataController = new DataController();
        dataController.LoadGame();
        SceneManager.LoadScene("Scenes/Game");

    }

    /**
     * 退出游戏
     * */
    public void QuitGame()
    {
        Application.Quit();
    }
}
