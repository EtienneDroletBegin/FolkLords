using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    private void Awake()
    {
        PartyManager.GetInstance().SpawnForCombat();
    }
}
