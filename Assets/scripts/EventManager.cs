using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EEvents
{
    ONPARTYSPAWN,
    ONBATTLESTART
}


public class EventManager : MonoBehaviour
{
    #region Singleton

    //1. Instance de la classe
    private static EventManager m_Instance;

    void Awake()
    {
        //2. Initialiser l'instance
        if (m_Instance == null)
        {
            m_Instance = this;
            //3. Faire persister l'instance
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //4. Detruire L'objet existant
            Destroy(gameObject);
        }
    }

    //5. Porte d'entree globale
    public static EventManager GetInstance()
    {
        return m_Instance;
    }

    #endregion


    Dictionary<EEvents, Action<Dictionary<string, object>>> m_Events = new Dictionary<EEvents, Action<Dictionary<string, object>>>();
    public void StartListening(EEvents eventName, Action<Dictionary<string, object>> eventToBind)
    {
        if (m_Events.ContainsKey(eventName))
        {
            m_Events[eventName] += eventToBind;
        }
        else
        {
            m_Events.Add(eventName, eventToBind);
        }
    }

    public void StopListening(EEvents eventName, Action<Dictionary<string, object>> eventToBind)
    {
        if (m_Events.ContainsKey(eventName))
        {
            m_Events[eventName] -= eventToBind;
            if (m_Events[eventName] == null)
            {
                m_Events.Remove(eventName);
            }
        }
    }

    public void TriggerEvent(EEvents eventName, Dictionary<string, object> eventParams)
    {
        if (m_Events.TryGetValue(eventName, out Action<Dictionary<string, object>> value))
        {
            value?.Invoke(eventParams);
        }
    }
}
