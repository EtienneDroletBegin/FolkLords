using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrustNobodyEffect : AbilityParent
{
    void Start()
    {
        unitCombatStats parent = transform.parent.GetComponent<unitCombatStats>();
        parent.AugmentAggro(2);
        parent.AttkGain(parent.getDMG());
    }

    private void OnDestroy()
    {
        unitCombatStats parent = transform.parent.GetComponent<unitCombatStats>();
        parent.AttkGain(parent.getDMG() * -1);
    }

}
