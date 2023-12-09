using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public struct initiative
{
    public ICombatUnit unit;
    public int init;
}

public class EncounterManager : MonoBehaviour
{
    #region Singleton

    //1. Instance de la classe
    private static EncounterManager m_Instance;

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
    public static EncounterManager GetInstance()
    {
        return m_Instance;
    }

    #endregion


    private List<initiative> m_encounter;
    private List<Monsters> m_monsters;
    [SerializeField]
    private GameObject BtPrefab;
    [SerializeField]
    private Monsters wendigo;
    private GameObject initIMG;
    private int CurrentTurn = 0;

    private void Start()
    {
        EventManager.GetInstance().StartListening(EEvents.TOGGLECOMBAT, ToggleCombat);
    }
    public void ToggleCombat(Dictionary<string, object> eventParams)
    {
        StartCoroutine(BeginFade());

    }

    public void startCombat()
    {
        m_encounter = new List<initiative>();
        EventManager.GetInstance().TriggerEvent(EEvents.TOGGLECOMBAT, null);

    }

    public void RollInitiative()
    {
        //REMOVE LATER-----------------------------
        m_encounter = new List<initiative>();
        m_monsters = new List<Monsters>();
        //------------------------------------------
        foreach (PartyMembers unit in PartyManager.GetInstance().getParty())
        {
            initiative ini = new initiative();
            ini.unit = unit;
            ini.init = UnityEngine.Random.Range(1, unit.spd);
            m_encounter.Add(ini);
        }

        int monsterNB = UnityEngine.Random.Range(1,3);
        //for (int i = 0; i<= monsterNB; i++)
        //{
        m_monsters.Add(wendigo);
        m_monsters.Add(wendigo);
        //}
        foreach(Monsters mnstr in m_monsters)
        {
            foreach(Transform spots in GameObject.Find("monsterSpots").transform)
            {
                if (spots.childCount == 0)
                {
                    Instantiate(Resources.Load<GameObject>("wendigo"), spots);
                    break;
                }
            }
            initiative ini = new initiative();
            ini.unit = mnstr;
            ini.init = UnityEngine.Random.Range(1, 6);
            m_encounter.Add(ini);
        }


        m_encounter = m_encounter.OrderByDescending(x => x.init).ToList();
        initIMG = Resources.Load<GameObject>("initIMG");
        foreach (initiative image in m_encounter)
        {
            Image go = Instantiate(initIMG, GameObject.Find("initiative").transform).GetComponent<Image>();
            if (image.unit is PartyMembers)
            {
                go.sprite = image.unit.ConvertTo<PartyMembers>().Portrait;
            }
            else
            {
                go.sprite = image.unit.ConvertTo<Monsters>().img;

            }
        }

        if (m_encounter[CurrentTurn].unit is Monsters)
        {
            endTurn();
        }

        foreach (Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }


    }

    public void endTurn()
    {
        foreach(Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }
        CurrentTurn++;
        if(CurrentTurn >= m_encounter.Count)
        {
            CurrentTurn = 0;
        }
        if (m_encounter[CurrentTurn].unit is Monsters)
        {
            endTurn();
        }
    }

    public void ChooseAbility()
    {
        foreach (Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Abilities abilities in m_encounter[CurrentTurn].unit.ConvertTo<PartyMembers>().abilities)
        {
            GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
            newBT.name = abilities.abilityName;
            newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = abilities.abilityName;
            newBT.GetComponent<BTStat>().SetAbilities(abilities);
            newBT.GetComponent<Button>().onClick.AddListener(delegate { abilities.ChooseTarget(); });
        }
    }

    public void ChooseTarget(Abilities ability)
    {
        foreach (Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }
        //No ability received so basic attack
        if(ability == null)
        {
            foreach (Transform mnstrs in GameObject.Find("monsterSpots").transform)
            {
                if (mnstrs.transform.childCount != 0)
                {
                    GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
                    newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mnstrs.GetChild(0).GetComponent<MnstrStats>()._name();
                    newBT.GetComponent<Button>().onClick.AddListener(delegate { attack(mnstrs); });
                }
            }
        }
        //Received an ability
        else
        {
            if(ability.Target.HasFlag(Abilities.ETarget.ENEMY))
            {
                foreach (Transform mnstrs in GameObject.Find("monsterSpots").transform)
                {
                    if (mnstrs.transform.childCount != 0)
                    {
                        GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
                        newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mnstrs.GetChild(0).GetComponent<MnstrStats>()._name();
                        newBT.GetComponent<Button>().onClick.AddListener(delegate { ability.Execute(mnstrs.GetChild(0).gameObject); });
                    }
                }
            }
            if (ability.Target.HasFlag(Abilities.ETarget.ALLY))
            {

            }
            if (ability.Target.HasFlag(Abilities.ETarget.SELF))
            {

            }
            if (ability.Target.HasFlag(Abilities.ETarget.AllyAll))
            {

            }
            if (ability.Target.HasFlag(Abilities.ETarget.EnemyAll))
            {

            }

        }

    }

    private void attack(Transform mnstrs)
    {
        print(m_encounter[CurrentTurn].unit.ConvertTo<PartyMembers>().physDMG);
        mnstrs.GetChild(0).GetComponent<MnstrStats>().TakeDamage(m_encounter[CurrentTurn].unit.ConvertTo<PartyMembers>().physDMG);
        endTurn();
    }

    IEnumerator BeginFade()
    {
        Color startColor = Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);
        float ElapsedTime = 0.0f;
        float TotalTime = 1f;


        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, (ElapsedTime/TotalTime));
            yield return null;
        }
        if(SceneManager.GetActiveScene().name != "FightScene")
        {
            SceneManager.LoadScene("FightScene");
        }
        else
        {
            SceneManager.LoadScene("OverWorld");
        }
        
    }

    
}
