using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlightenEffect : AbilityParent
{
    void Start()
    {
        int healing = Random.Range(2, 11);
        transform.parent.GetComponent<unitCombatStats>().Heal(healing);
    }

}
