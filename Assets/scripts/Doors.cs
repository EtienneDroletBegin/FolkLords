using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    [SerializeField]private Monsters[] monsters;

    private bool isTriggered = false;
    private Image EncounterBackground;
    
    private void Update()
    {
        if(isTriggered)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SaveData dataToSave = new SaveData(transform.position, PartyManager.GetInstance().getParty());
                SaveSystem.save(dataToSave);
                Debug.Log(dataToSave.Position.ToString());
                EncounterManager.GetInstance().ToggleCombat(null);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("mainPlayer"))
        {
            isTriggered = true;
            GetComponent<TextMeshPro>().SetText("Press F");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("mainPlayer"))
        {
            isTriggered = false;
            GetComponent<TextMeshPro>().SetText("");
        }
    }
}
