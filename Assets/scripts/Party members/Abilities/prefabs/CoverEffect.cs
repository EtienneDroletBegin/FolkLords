using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverEffect : AbilityParent
{
    void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().Cover();
    }

}
