using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstocEffect : AbilityParent
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<MnstrStats>().TakeDamage(castableBy.physDMG);
        transform.parent.GetComponent<MnstrStats>().ReduceDamage(1);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<MnstrStats>().ReduceDamage(-1);
        
    }
}
