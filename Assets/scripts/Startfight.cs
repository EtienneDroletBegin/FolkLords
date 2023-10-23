using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startfight : MonoBehaviour
{
    public void StartBattle()
    {
        EventManager.GetInstance().TriggerEvent(EEvents.ONBATTLESTART, null);
    }


}
