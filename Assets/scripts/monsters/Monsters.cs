using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Monsters : ScriptableObject, ICombatUnit
{
    public string monsterName;
    public int BaseHP;
    public int BaseAtk;
    public Sprite img;
    public Abilities[] moves;
}
