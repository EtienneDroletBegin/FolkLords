using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShotEffect : AbilityParent
{
    void Start()
    {
        int damage = Random.Range(castableBy.physDMG-3, castableBy.physDMG);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
    }

}
