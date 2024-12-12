using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyChatBlock : MonoBehaviour, IChatBlockUI
{
    [SerializeField] private TMP_Text msgText;
    public void Init(ChatData chatData)
    {
        msgText.text = chatData.msg;
    }
}
