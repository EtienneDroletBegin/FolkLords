using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmEffect : AbilityParent
{
    // Start is called before the first frame update
    void Start()
    {
        int damage = Random.Range(2, 4);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
        transform.parent.GetComponent<MnstrStats>().ReduceDamage(1);
    }
}
