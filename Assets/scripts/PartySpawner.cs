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
                    Follow F2 =newMember.AddComponent<Follow>();
                    F2.SetTarget(GameObject.FindGameObjectWithTag("follow1").transform) ;
                    break;
            }
            currentMember++;
        }

    }
}
