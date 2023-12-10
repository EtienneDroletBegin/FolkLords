using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masseffect : AbilityParent
{
    void Start()
    {
        int healing = Random.Range(2, 6);
        transform.parent.GetComponent<unitCombatStats>().Heal(healing);
    }

}
