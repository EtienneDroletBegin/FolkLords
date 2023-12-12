using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenteEffect : AbilityParent
{
    void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().AttkGain(castableBy.physDMG);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<unitCombatStats>().AttkGain(castableBy.physDMG * -1);
    }

}
