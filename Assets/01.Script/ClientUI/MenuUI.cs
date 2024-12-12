using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private List<Button> menuButtonList;
    [SerializeField] private List<GameObject> menuObjList;

    private int nowObj;
    void Start()
    {
        nowObj = 0;
        if (menuButtonList.Count != menuObjList.Count)
            return;

        int len = menuObjList.Count;

        for(int i = 0; i < len; i++)
        {
            int idx = i;
            if(nowObj == idx)
            {
                menuButtonList[idx].GetComponent<Image>().color = new Color32(95, 95, 95,255);
            }
            menuButtonList[idx].onClick.AddListener(() =>
            {
                menuObjList[nowObj].SetActive(false);
                menuObjList[idx].SetActive(true);

                menuButtonList[nowObj].GetComponent<Image>().color = new Color32(202, 202, 202, 255);
                menuButtonList[idx].GetComponent<Image>().color = new Color32(95, 95, 95, 255);

                nowObj = idx;
            });
            AddEventTrigger(menuButtonList[idx].gameObject, idx);
        }
    }
    public void AddEventTrigger(GameObject obj,int idx)
    {
        EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
        EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();

        entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
        entry_PointerExit.eventID = EventTriggerType.PointerExit;

        entry_PointerEnter.callback.AddListener((data) =>
        {
            if (idx == nowObj) return;

            obj.GetComponent<Image>().color = new Color32(131, 131, 131, 255);

        });
        entry_PointerExit.callback.AddListener((data) =>
        {
            if (idx == nowObj) return;

            obj.GetComponent<Image>().color = new Color32(202, 202, 202, 255);
        });

        obj.GetComponent<EventTrigger>().triggers.Add(entry_PointerEnter);
        obj.GetComponent<EventTrigger>().triggers.Add(entry_PointerExit);
    }
}
