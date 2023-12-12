using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unnaturalPresenceEffect : MonoBehaviour
{
    void Start()
    {
        transform.parent.GetComponent<MnstrStats>().TakeDamage(-25);
    }

}
