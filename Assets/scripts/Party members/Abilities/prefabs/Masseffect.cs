using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masseffect : AbilityParent
{
    void Start()
    {
        int healing = Random.Range(castableBy.magDMG-5, castableBy.magDMG-2);
        transform.parent.GetComponent<unitCombatStats>().Heal(healing);
    }

}
