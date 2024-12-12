using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChatData
{
    public string nickName;
    public string msg;

    //public ChatData() { }
    public ChatData(string  nickName, string msg)
    {
        this.nickName = nickName;
        this.msg = msg;
    }
}
