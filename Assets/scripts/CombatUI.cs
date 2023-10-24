using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    private void Start()
    {
        AudioManager.GetInstance().fadeToNext("BattleSong");
    }
    public void Flee()
    {
        EventManager.GetInstance().TriggerEvent(EEvents.TOGGLECOMBAT, null);
        AudioManager.GetInstance().fadeToNext("OverWorldSong");
        
    }
}
