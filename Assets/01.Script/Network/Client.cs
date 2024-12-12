using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using UnityEngine.XR;
public class Client : MonoBehaviour
{
    [SerializeField] private ClientUIManager UIManager;
    Socket socket;
    private string serverIP;
    private int serverPort;
    Thread threadReceive;

    void Start()
    {
        serverIP = "127.0.0.1";
        socket = null;
    }
    public void SetServerIP(string IP)
    {
        serverIP = IP;
    }
    public void SetServerPort(int Port)
    {
        serverPort = Port;
    }

    public void Connect()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(serverIP, serverPort);

            threadReceive = new Thread(Receive);
            threadReceive.Start();

            Send(UIManager.NickName);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        UIManager.OpenRoom();
    }
    public void Disconnect()
    {
        socket.Close();
        threadReceive.Abort();
    }
    public void Send(string msg)
    {
        try
        {
            byte[] senddata = Encoding.Default.GetBytes(msg);

            socket.Send(senddata, senddata.Length, SocketFlags.None);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    public void Receive()
    {
        int retval;
        byte[] buf = new byte[1024];
        while (true)
        {
            Array.Clear(buf, 0x0, buf.Length);
            retval = socket.Receive(buf, 1024, SocketFlags.None);
            if (retval == 0) break;

            string msg = Encoding.Default.GetString(buf);
            Debug.Log(msg);
            UnityMainThreadDispatcher.Instance.Enqueue(() => { Deserialization(msg); });
            
        }
    }
    void Deserialization(string msg)
    {
        string[] data = msg.Split('|');
        
        switch (data[0])
        {
            case "CONNECT":
                UIManager.AddBlock(ChatRoomBlockType.Notification, new ChatData(data[1], "이가 들어왔습니다."));
                UIManager.SetUserCount(int.Parse(data[2]));
                break;
            case "DATA":
                if (data[1] == UIManager.NickName)
                    UIManager.AddBlock(ChatRoomBlockType.MyChat, new ChatData(data[1], data[2]));
                else
                    UIManager.AddBlock(ChatRoomBlockType.OtherChat, new ChatData(data[1], data[2]));
                break;
            case "DISCONNECT":
                UIManager.AddBlock(ChatRoomBlockType.Notification, new ChatData(data[1], "님이 나갔습니다."));
                UIManager.SetUserCount(int.Parse(data[2]));
                break;
        }
    }
    private void OnApplicationQuit()
    {
        if (socket == null) return;
        if (socket.Connected)
        {
            Disconnect();
        }

        Debug.Log("[Log] Server Close");
    }

}
