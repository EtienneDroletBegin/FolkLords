using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomOmenEffect : mnstrAbilityParent
{
    private void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().TakeDamage(10);
    }
}
