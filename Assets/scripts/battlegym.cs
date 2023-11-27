using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlegym : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EncounterManager.GetInstance().startCombat();
    }
}
