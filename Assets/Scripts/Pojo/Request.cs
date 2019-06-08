using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Request
{
    private int code;  // 请求代号
    private string content;  // 请求描述

    public Request() { }
    public Request(int code, string content)
    {
        this.code = code;
        this.content = content;
    }

    public int Code { get => code; set => code = value; }
    public string Content { get => content; set => content = value; }
}
