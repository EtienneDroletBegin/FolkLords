using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    #region Singleton

    //1. Instance de la classe
    private static PartyManager m_Instance;

    void Awake()
    {
        //2. Initialiser l'instance
        if (m_Instance == null)
        {
            m_Instance = this;
            //3. Faire persister l'instance
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //4. Detruire L'objet existant
            Destroy(gameObject);
        }
    }

    //5. Porte d'entree globale
    public static PartyManager GetInstance()
    {
        return m_Instance;
    }

    #endregion

    //[SerializeField]private List<PartyMembers> activeParty;
    [SerializeField] private PartyMembers[] ActiveParty = { null, null, null }; 
    [SerializeField] private List<PartyMembers> AvailableChars;

    [SerializeField] private UnityEngine.UI.Image[] Selected;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Button goButton;

    public void AddToParty(int memberInt)
    {
        if (ActiveParty.Contains(null)) 
        {
            //var enums = (EPartyMember[])Enum.GetValues(typeof(EPartyMember));

            for (int i = 0; i<=3; i++) 
            {
                if (Selected[i].sprite == null)
                {
                    ActiveParty[i] = AvailableChars[memberInt];

                    Selected[i].sprite = ActiveParty[i].Portrait;
                    Debug.Log(ActiveParty[i].memberName);
                    GameObject.Find("ActiveTexts").transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = ActiveParty[i].memberName;
                    GameObject.Find("buttons").transform.GetChild(memberInt).GetComponent<Button>().interactable = false;
                    break;
                }
            }

            if (!ActiveParty.Contains(null))
            {
                goButton.interactable = true;
            }
            
        }

        

    }

    public PartyMembers[] getParty()
    {
        return ActiveParty;
    }

    public void RemoveFromParty(int index)
    {
        GameObject.Find("buttons").transform.GetChild(ActiveParty[index].index).GetComponent<Button>().interactable = true;
        GameObject.Find("ActiveTexts").transform.GetChild(index).GetComponent<TextMeshProUGUI>().text = "";
        goButton.interactable = false;
        ActiveParty[index] = null;
        Selected[index].sprite = null;
    }

    public void ConfirmPartyChange()
    {
        Debug.Log("load scende");
        Selected = null;
        SceneManager.LoadScene("OverWorld");
    }


    
    
    public void Spawn(Vector2 position)
    {
        
        int currentMember = 0;
        
        foreach (PartyMembers member in ActiveParty)
        {
            GameObject newMember = Instantiate(member.prefab);
            position = position + new Vector2((1 * currentMember) * newMember.GetComponent<SpriteRenderer>().bounds.size.x, 0);
            newMember.transform.position = position;
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
