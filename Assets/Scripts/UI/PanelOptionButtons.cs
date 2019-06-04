using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 监听选项面板button事件
 * */
public class PanelOptionButtons : MonoBehaviour
{
    [SerializeField] PauseController pauseController;
    [SerializeField] GameObject Panel;
    public DataController dataController;

    /**
     * 监听返回游戏button
     * */
    public void ReturnButtonPressed()
    {
        if (pauseController.Paused)
        {
            Panel.gameObject.SetActive(false);
            pauseController.StartGame();
            pauseController.Paused = false;
        }
    }
    /**
     * 监听保存游戏button
     * */
    public void SaveButtonPressed()
    {
        Debug.Log("SaveButtonPressed");
        dataController.SaveGame();
    }

    /**
     * 监听返回菜单button
     * */
    public void MenuButtonPressed()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }

    /**
     * 监听返回桌面button
     * */
    public void WindowButtonPressed()
    {
        Application.Quit();
    }
}
