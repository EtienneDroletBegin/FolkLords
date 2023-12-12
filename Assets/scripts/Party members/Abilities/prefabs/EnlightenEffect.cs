using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlightenEffect : AbilityParent
{
    void Start()
    {
        int healing = Random.Range(castableBy.magDMG-2, castableBy.magDMG+4);
        transform.parent.GetComponent<unitCombatStats>().Heal(healing);
    }

}
