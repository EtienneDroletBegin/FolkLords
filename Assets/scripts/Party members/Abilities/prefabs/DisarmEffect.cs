using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmEffect : AbilityParent
{
    // Start is called before the first frame update
    void Start()
    {
        int damage = Random.Range(castableBy.physDMG-2, castableBy.physDMG);
        transform.parent.GetComponent<MnstrStats>().TakeDamage(damage);
        transform.parent.GetComponent<MnstrStats>().ReduceDamage(1);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<MnstrStats>().ReduceDamage(-1);
    }
}
