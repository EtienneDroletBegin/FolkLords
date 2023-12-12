using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffect : AbilityParent
{
    // Start is called before the first frame update
    void Start()
    {
        int damage = Random.Range(castableBy.physDMG-3, castableBy.physDMG);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
    }

}
