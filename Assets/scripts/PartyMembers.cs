using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu]
public class PartyMembers : ScriptableObject, ICombatUnit
{
    [SerializeField] public int index;
    [SerializeField] public string memberName;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxAP;
    [SerializeField] public int physDMG;
    [SerializeField] public int magDMG;
    [SerializeField] public int spd;
    [SerializeField] public Sprite Portrait;
    [SerializeField]public RuntimeAnimatorController controller;

    [SerializeField] public GameObject prefab;

    public struct AbilityUnlocks
    {
        public int Level;
        public Abilities ability;
    }

    public Abilities[] abilities;

}


[CreateAssetMenu]
public class Abilities : ScriptableObject
{
    [Flags]
    public enum ETarget
    {
        ENEMY = 1 << 0,
        ALLY = 1 << 1,
        SELF = 1 << 2,
        EnemyAll = 1 << 3,
        AllyAll = 1 << 4,
    }

    public ETarget Target;
    public string abilityName;
    public GameObject prefab;
    public int APCost;


    public void ChooseTarget()
    {
        EncounterManager.GetInstance().ChooseTarget(this);
    }

    public void Execute(List<Transform> target, string caster)
    {
        foreach (Transform t in target) 
        {
            Instantiate(prefab, t);
        }
        GameObject abilityUI = GameObject.Find("AbilityUI");
        abilityUI.gameObject.GetComponent<Animation>().Play();
        abilityUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = caster + " has used " + abilityName;
        //EncounterManager.GetInstance().endTurn();
    }

}
