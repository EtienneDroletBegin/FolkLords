using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLightEffect : AbilityParent
{
    void Start()
    {
        int damage = Random.Range(castableBy.magDMG - 3, castableBy.magDMG);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
    }

}
