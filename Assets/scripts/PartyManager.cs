using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum EPartyMember
{
    Pope,
    Musketeer,
    Drifter,
    Conspirationist,
    Rifleman
}


public class PartyManager : MonoBehaviour
{
    [SerializeField]private List<PartyMembers> activeParty;
    [SerializeField] private List<PartyMembers> AvailableChars;

    [SerializeField] private UnityEngine.UI.Image[] Selected;
    [SerializeField] private Sprite[] images;


    public void AddToParty(int memberInt)
    {
        if (activeParty.Count < 3) 
        {
            //var enums = (EPartyMember[])Enum.GetValues(typeof(EPartyMember));

            activeParty.Add(AvailableChars[memberInt]);

            for (int i = 0; i<=3; i++) 
            {
                if (Selected[i].sprite == null)
                {
                    Selected[i].sprite = activeParty[i].Portrait;
                    break;
                }
            }

            //return;
        }



    }
}
