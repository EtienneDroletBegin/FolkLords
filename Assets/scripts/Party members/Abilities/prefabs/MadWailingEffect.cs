using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadWailingEffect : AbilityParent
{
    void Start()
    {
        int damage = Random.Range(4, 8);
        if (transform.parent.GetComponent<MnstrStats>() != null)
        {
            transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
        }
        else
        {
            transform.parent.GetComponent<unitCombatStats>().TakeDamage(damage);
        }

    }

}
