using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 开始页面按钮事件监听
 * */
public class MainMenuButton : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void LoadGame()
    {
        PlayerData playerData = new PlayerData();
        playerData.LoadGame();
    
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
