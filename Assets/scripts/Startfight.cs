using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startfight : MonoBehaviour
{
    void Start()
    {
        EventManager.GetInstance().TriggerEvent(EEvents.ONBATTLESTART, new Dictionary<string, object>());
    }


}
