using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraceEffect : AbilityParent
{
    void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().AugmentAggro(2);
        transform.parent.GetComponent<unitCombatStats>().ResistanceGain(1);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<unitCombatStats>().ResistanceGain(-1);
    }


}
