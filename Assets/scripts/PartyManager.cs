using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public enum EPartyMember
{
    Pope,
    Musketeer,
    Drifter,
    Conspirationist,
    Rifleman
}

public class Abilities : ScriptableObject
{

}



[CreateAssetMenu]
public class PartyMember : ScriptableObject
{
    [SerializeField]int maxHP;
    [SerializeField] int maxAP;
    [SerializeField] int physDMG;
    [SerializeField] int magDMG;

    [SerializeField]GameObject prefab;
    Abilities[] abilities;
}


public class PartyManager : MonoBehaviour
{
    [SerializeField]private List<EPartyMember> activeParty;


    public void AddToParty(int memberInt)
    {
        var enums = (EPartyMember[])Enum.GetValues(typeof(EPartyMember));

        activeParty.Add(enums[memberInt]);
    }
}
