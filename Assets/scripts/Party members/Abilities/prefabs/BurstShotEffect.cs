using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShotEffect : AbilityParent
{
    void Start()
    {
        int damage = Random.Range(3, 7);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
    }

}
