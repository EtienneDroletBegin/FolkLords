using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultEffects : AbilityParent
{
    void Start()
    {
        int times = Random.Range(2, 5);
        StartCoroutine("Assaut", times);
    }



    IEnumerator Assaut(int times)
    {
        for (int i = 0; i < times; i++)
        {
            transform.parent.GetComponent<MnstrStats>().TakeDamage(castableBy.physDMG);
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
        
    }
}
