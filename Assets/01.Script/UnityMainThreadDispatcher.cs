using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// By Chatgpt
public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> actions = new Queue<Action>();
    private static UnityMainThreadDispatcher instance;
    
    public static UnityMainThreadDispatcher Instance
    {
        get
        {
            return instance;
            
        }
    }
    // ¼öÁ¤ºÎ
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Enqueue(Action action)
    {
        
        lock (actions)
        {
            actions.Enqueue(action);
        }
    }

    private void Update()
    {
        while (actions.Count > 0)
        {
            Action action;
            lock (actions)
            {
                action = actions.Dequeue();
            }
            action.Invoke();
        }
    }
}
