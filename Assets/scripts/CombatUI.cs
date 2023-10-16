using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    public void Flee()
    {
        EventManager.GetInstance().TriggerEvent(EEvents.TOGGLECOMBAT, null);
    }
}
