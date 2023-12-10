using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrustNobodyEffect : AbilityParent
{
    void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().AugmentAggro(1);
    }

}
