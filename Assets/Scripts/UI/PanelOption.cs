using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中选项显示控制
/// </summary>
public class PanelOption : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] PauseController pauseController;
    private InputController playerInput;

    private void Awake()
    {
        Panel.SetActive(false);
        playerInput = GameManager.Instance.InputController;
    }

    // Update is called once per frame
    void Update()
    {
        ShowPanel();
    }

    /**
     * 控制设置面板是否显示
     * */
    private void ShowPanel()
    {
        if (playerInput.EscDown)
        {
            Debug.Log("pree esc");
            if (pauseController.Paused)
            {
                Panel.gameObject.SetActive(false);
                pauseController.StartGame();
                pauseController.Paused = false;
            }
            else
            {
                Panel.gameObject.SetActive(true);
                pauseController.PauseGame();
                pauseController.Paused = true;
            }

        }

    }
}
