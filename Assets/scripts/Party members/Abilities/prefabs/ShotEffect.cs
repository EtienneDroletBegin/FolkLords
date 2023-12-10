using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffect : AbilityParent
{
    // Start is called before the first frame update
    void Start()
    {
        int damage = Random.Range(5, 9);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
    }

}
