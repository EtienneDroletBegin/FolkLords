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

    public void Defend()
    {
        EncounterManager.GetInstance().endTurn();

    }

    public void Attack()
    {
        EncounterManager.GetInstance().ChooseTarget(null);

    }

    public void Special()
    {
        EncounterManager.GetInstance().ChooseAbility();
    }

}
