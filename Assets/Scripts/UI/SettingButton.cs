using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 按钮事件监听
/// </summary>
[RequireComponent(typeof(DataController))]
public class SettingButton : MonoBehaviour
{
    public DataController dataController;

    public void BackGame()
    {
        SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }

    public void SaveGame()
    {
        dataController.SaveGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
