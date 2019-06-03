using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerData))]
public class SettingButton : MonoBehaviour
{
    public PlayerData playerData;

    public void BackGame()
    {
        SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }

    public void SaveGame()
    {
        playerData.SaveGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
