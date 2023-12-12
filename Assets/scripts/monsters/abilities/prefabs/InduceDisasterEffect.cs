using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InduceDisasterEffect : MonoBehaviour
{
    private void Start()
    {
        transform.parent.GetComponent<unitCombatStats>().TakeDamage(15);
    }
}
