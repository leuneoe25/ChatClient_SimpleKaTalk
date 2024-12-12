using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatRoom : MonoBehaviour
{
    [SerializeField] private ClientUIManager UIManager;
    [SerializeField] private Button ExitButton;
    [Header("Content")]
    [SerializeField] private GameObject ContentObj;
    [Header("Count")]
    [SerializeField] private TMP_Text userCountText;
    [Header("Ipnut")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button inputSendButton;
    [Header("Block")]
    [SerializeField] private GameObject NotificationBlock;
    [SerializeField] private GameObject OtherChatBlock;
    [SerializeField] private GameObject MyChatBlock;
    void Start()
    {
        inputField.onSubmit.AddListener((msg) =>
        {
            UIManager.SendInput(msg);
            inputField.text = string.Empty;
            inputField.ActivateInputField();
        });
        inputField.onEndEdit.AddListener((msg) =>
        {
            if (msg == string.Empty) return;
            else inputSendButton.interactable = true;
            
        });
        inputSendButton.onClick.AddListener(() =>
        {
            UIManager.SendInput(inputField.text);
            inputSendButton.interactable = false;
            inputField.text = string.Empty;
        });
        ExitButton.onClick.AddListener(() =>
        {
            UIManager.Disconnect();
            gameObject.SetActive(false);
        });
    }

    public void SetUserCount(int count)
    {
        userCountText.text = "User : " + count.ToString();
    }
    public void AddBlock(ChatRoomBlockType type, ChatData cd)
    {
        GameObject obj = null;
        switch (type)
        {
            case ChatRoomBlockType.Notification:
                obj = Instantiate(NotificationBlock, ContentObj.transform);
                break;
            case ChatRoomBlockType.OtherChat:
                obj = Instantiate(OtherChatBlock, ContentObj.transform);
                break;
            case ChatRoomBlockType.MyChat:
                obj = Instantiate(MyChatBlock, ContentObj.transform);
                break;
        }
        obj.GetComponent<IChatBlockUI>().Init(cd);
    }
}
public enum ChatRoomBlockType
{
    Notification,
    OtherChat,
    MyChat
}