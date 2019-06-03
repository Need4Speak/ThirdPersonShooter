using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestoryObject : MonoBehaviour
{
    [SerializeField] PauseController pauseController;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(GameManager.Instance.InputController);
        //DontDestroyOnLoad(GameManager.Instance.LocalPlayer);
        //DontDestroyOnLoad(GameManager.Instance.Respawner);
        //DontDestroyOnLoad(GameManager.Instance.Timer);

        DontDestroyOnLoad(pauseController);
        //DontDestroyOnLoad(text);
    }
}
