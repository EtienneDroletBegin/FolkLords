using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{

    [SerializeField]
    private GameObject bgPrefab;
    private Sprite Background;
    private void Awake()
    {
        PartyManager.GetInstance().SpawnForCombat();
        EncounterManager.GetInstance().RollInitiative();
        
    }
    private void Start()
    {
        Instantiate(bgPrefab, GameObject.Find("BackgroundPos").transform);
        
    }
}
