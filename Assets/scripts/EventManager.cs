using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;
    private static EventManager instance;

    #region Singleton
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();

                if(instance == null)
                {
                    instance = new GameObject("EventManager").AddComponent<EventManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        if (!instance)
        {
            return;
        }

        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> eventParams)
    {
        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(eventParams);
        }
    }

}
