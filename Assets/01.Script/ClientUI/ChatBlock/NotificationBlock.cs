using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NotificationBlock : MonoBehaviour, IChatBlockUI
{
    [SerializeField] private TMP_Text msgText;
    public void Init(ChatData chatData)
    {
        Debug.Log(chatData.msg);

        chatData.nickName = chatData.nickName.Replace("\0", string.Empty);
        msgText.text =  chatData.nickName+ chatData.msg;
    }
}
