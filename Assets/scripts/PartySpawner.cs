using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PartySpawner : MonoBehaviour
{
    private PartyMembers[] party;
    private void Start()
    {
        EventManager.StartListening("Spawn", Spawn);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("Spawn", Spawn);
    }
    public void Spawn(Dictionary<string,object> eventParams)
    {
        party = PartyManager.GetInstance().getParty();
        int currentMember = 0;
        object position;
        if(eventParams.TryGetValue("Position", out position)) 
        {
            position = (Vector2)position;
        }
        foreach (PartyMembers member in party)
        {
            GameObject newMember = Instantiate(member.prefab);
            position = (Vector2)position + new Vector2((1 * currentMember) * newMember.GetComponent<SpriteRenderer>().bounds.size.x, 0);
            newMember.transform.position = (Vector2)position;
            newMember.GetComponent<Animator>().runtimeAnimatorController = member.controller;

            switch (currentMember)
            {
                case 0:
                    newMember.AddComponent<PlayerMovement>();
                    break;
                case 1:
                    Follow F = newMember.AddComponent<Follow>();
                    F.tag = "follow1";
                    F.SetTarget(FindAnyObjectByType<PlayerMovement>().transform);
                    break;
                case 2:
                    Follow F2 = newMember.AddComponent<Follow>();
                    F2.SetTarget(GameObject.FindGameObjectWithTag("follow1").transform);
                    break;
            }
            currentMember++;
        }

    }
}
