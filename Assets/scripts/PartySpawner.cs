using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PartySpawner : MonoBehaviour
{
    private PartyMembers[] party;
    void Start()
    {
        party = PartyManager.GetInstance().getParty();
        int currentMember = 0;

        foreach (PartyMembers member in party)
        {
            GameObject newMember = Instantiate(member.prefab);
            newMember.transform.position = new Vector3((1 * currentMember) * newMember.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
            switch (currentMember)
            {
                case 0:
                    newMember.AddComponent<PlayerMovement>();
                    break;
                case 1:
                    newMember.AddComponent<Follow>();
                    newMember.GetComponent<Follow>().SetTarget(FindAnyObjectByType<PlayerMovement>().transform);
                    break;
                case 2:
                    newMember.AddComponent<Follow>();
                    newMember.GetComponent<Follow>().SetTarget(FindAnyObjectByType<Follow>().transform);
                    break;
            }
            currentMember++;
        }

    }
}
