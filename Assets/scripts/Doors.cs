using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    private Monsters[] monsters;

    private bool isTriggered = false;
    private Sprite EncounterBackground;
    
    private void Update()
    {
        if(isTriggered)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SaveData dataToSave = new SaveData(transform.position, PartyManager.GetInstance().getParty());
                SaveSystem.save(dataToSave);
                Debug.Log(dataToSave.Position.ToString());

                Dictionary<string,object> data = new Dictionary<string,object>();
                data.Add("Monsters", monsters);
                data.Add("bg", EncounterBackground);
                EncounterManager.GetInstance().ToggleCombat(data);
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
    public void SetEncounterMonsters(Monsters[] _monsters)
    {
        monsters = _monsters;
    }
    public void SetBackground(Sprite bg)
    {
        EncounterBackground = bg;
    }
}
