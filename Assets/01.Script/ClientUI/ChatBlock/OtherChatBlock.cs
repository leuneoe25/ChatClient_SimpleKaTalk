using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OtherChatBlock : MonoBehaviour, IChatBlockUI
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text msgText;

    public void Init(ChatData chatData)
    {
        nameText.text = chatData.nickName;
        msgText.text = chatData.msg;
    }

}
