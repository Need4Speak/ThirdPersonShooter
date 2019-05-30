using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 进入设置页面
 * */
public class Setting : MonoBehaviour
{
    public InputController playerInput;

    private void Awake()
    {
        playerInput = GameManager.Instance.InputController;
    }

    private void Update()
    {
        EnterSetting();
    }

    /**
     * 进入设置页面
     * */
    void EnterSetting()
    {
        if(playerInput.EscDown) { 
            SceneManager.LoadScene("Scenes/Setting", LoadSceneMode.Additive);
        }
    }
}
