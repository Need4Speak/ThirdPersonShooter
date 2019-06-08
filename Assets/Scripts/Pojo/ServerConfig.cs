using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConfig
{
    private string ip;
    private int port;

    public string Ip { get => ip; set => ip = value; }
    public int Port { get => port; set => port = value; }

    public ServerConfig()
    {
    }

    public ServerConfig(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
    }

    public override string ToString()
    {
        return "ip:" + ip + ", port: " + port;
    }
}
 