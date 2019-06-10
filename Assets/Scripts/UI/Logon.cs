using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class Logon : MonoBehaviour
{
    public GameObject useridObj;  // 账号
    public GameObject passwordObj;  // 密码

    private string userid;
    private string password;

    public GameObject LoadFailMsg;  // 登陆失败信息

    private void Awake()
    {
        LoadFailMsg.SetActive(false);
    }

    /// <summary>
    /// 处理用户按下登录按钮后逻辑
    /// </summary>
    public void PressLogon()
    {
        userid = useridObj.GetComponent<InputField>().text.Trim();
        password = passwordObj.GetComponent<InputField>().text.Trim();
        Debug.Log("userid: " + userid + ", password: " + password);

        if (ProcessLogon(userid, password))
        {
            Debug.Log("登陆成功");
            GlobalObjectControl.Instance.Userid = userid;
            SceneManager.LoadScene("Scenes/StartMenu");
        } else
        {
            Debug.Log("登陆失败");
            LoadFailMsg.SetActive(true);
        }
    }

    /// <summary>
    /// 判断登录是否合法
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool ProcessLogon(string userid, string password)
    {
        if(!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(password) 
            && "123".Equals(password))
        {
            return true;
        }
        return false;
    }
}
