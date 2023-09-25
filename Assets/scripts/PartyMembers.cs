using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


[CreateAssetMenu]
public class PartyMembers : ScriptableObject
{
    [SerializeField] public int index;
    [SerializeField] public string memberName;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxAP;
    [SerializeField] public int physDMG;
    [SerializeField] public int magDMG;
    [SerializeField] public Sprite Portrait;
    [SerializeField]public RuntimeAnimatorController controller;

    [SerializeField] public GameObject prefab;
    Abilities[] abilities;

}


public class Abilities : ScriptableObject
{

}
