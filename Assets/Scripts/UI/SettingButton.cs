using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public void BackGame()
    {
        SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }

    public void SaveGame()
    {
        Debug.Log("save game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
