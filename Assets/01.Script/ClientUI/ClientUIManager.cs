using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ClientUIManager : MonoBehaviour
{
    [SerializeField] private Client clientSystem;
    [SerializeField] private ChatRoom chatRoomSystem;
    [Header("MenuUI")]

    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_InputField nickNameInputField;
    [SerializeField] private TMP_InputField serverIPInputField;
    [SerializeField] private TMP_InputField serverPortInputField;
    [SerializeField] private Button serverConnectButton;


    [SerializeField] private string nickName;
    public string NickName {  
        get { return nickName; } 
        set {  nickName = value; nickNameText.text = value; } 
    }
    void Start()
    {
        serverIPInputField.onEndEdit.AddListener((value) =>
        {
            if (value == string.Empty) return;
            clientSystem.SetServerIP(value);
        });
        serverPortInputField.onEndEdit.AddListener((value) =>
        {
            if (value == string.Empty) return;
            clientSystem.SetServerPort(int.Parse(value));
        });
        serverConnectButton.onClick.AddListener(() =>
        {
            clientSystem.Connect();
        });
        NickName = System.Guid.NewGuid().ToString();
        nickNameInputField.text = nickName;
        nickNameInputField.onEndEdit.AddListener((value) =>
        {
            if(value != string.Empty)
            {
                NickName = value;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBlock(ChatRoomBlockType type,ChatData cd)
    {
        chatRoomSystem.AddBlock(type, cd);
    }
    public void OpenRoom()
    {
        chatRoomSystem.gameObject.SetActive(true);
    }
    public void SetUserCount(int count)
    {
        chatRoomSystem.SetUserCount(count);
    }
    public void SendInput(string text)
    {
        if(text == string.Empty) return;
        clientSystem.Send($"DATA|{nickName}|{text}");
    }
    public void Disconnect()
    {
        clientSystem.Disconnect();
    }
}
