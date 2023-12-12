using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public GameObject prefab;
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
    private Monsters[] m_monsters;
    [SerializeField]
    private GameObject BtPrefab;

    private GameObject initIMG;
    private int CurrentTurn = 0;
    private float AP = 0;
    private float BurnedAP = 0;
    private float MaxAp = 5;
    private GameObject APGauge;
    private TextMeshProUGUI burnText;
    private List<Image> iniImages;
    private Image currentTurnImage;

    private void Start()
    {
        EventManager.GetInstance().StartListening(EEvents.TOGGLECOMBAT, ToggleCombat);

    }
    public void ToggleCombat(Dictionary<string, object> eventParams)
    {

        if (eventParams != null)
        {
            object param;
            if (eventParams.TryGetValue("Monsters", out param))
            {
                m_monsters = param.ConvertTo<Monsters[]>();
            }
            if (eventParams.TryGetValue("bg", out param))
            {

            }

        }


        StartCoroutine(BeginFade());

    }

    public float GetAP() { return AP; }
    public float GetMaxAP() { return MaxAp; }

    public void startCombat()
    {
        m_encounter = new List<initiative>();

        EventManager.GetInstance().TriggerEvent(EEvents.TOGGLECOMBAT, null);

    }

    public void RollInitiative()
    {
        AP = 0;
        APGauge = GameObject.Find("APGauge");
        burnText = GameObject.Find("BurnText").GetComponent<TextMeshProUGUI>();
        burnText.gameObject.SetActive(false);
        //REMOVE LATER-----------------------------
        m_encounter = new List<initiative>();
        //------------------------------------------
        foreach (PartyMembers unit in PartyManager.GetInstance().getParty())
        {
            initiative ini = new initiative();
            ini.unit = unit;
            ini.init = UnityEngine.Random.Range(1, unit.spd);
            ini.prefab = GameObject.Find(unit.memberName);
            m_encounter.Add(ini);
        }


        foreach (Monsters mnstr in m_monsters)
        {
            initiative ini = new initiative();
            ini.unit = mnstr;
            ini.init = UnityEngine.Random.Range(1, 6);
            foreach (Transform spots in GameObject.Find("monsterSpots").transform)
            {
                if (spots.childCount == 0)
                {
                    ini.prefab = Instantiate((GameObject)Resources.Load(mnstr.monsterName), spots);
                    break;
                }
            }
            m_encounter.Add(ini);

        }
        m_encounter = m_encounter.OrderByDescending(x => x.init).ToList();
        initIMG = Resources.Load<GameObject>("initIMG");
        int index = 0;
        iniImages = new List<Image>();
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
            go.name = "initImg" + index++;
            iniImages.Add(go);
        }
        currentTurnImage = Instantiate(Resources.Load("currentTurn"), GameObject.Find("Canvas").transform).GetComponent<Image>();
        

        if (m_encounter[CurrentTurn].unit is Monsters)
        {
            List<initiative> aggro = m_encounter.Where(x => x.unit is PartyMembers).ToList();
            //aggro = ;
            m_encounter[CurrentTurn].prefab.GetComponent<MnstrStats>().Attack(aggro);
        }

        foreach (Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }
        currentTurnImage.transform.position = iniImages[0].transform.position;

    }

    public void endTurn()
    {
        foreach (Transform t in GameObject.Find("AbilityButtons").transform)
        {
            Destroy(t.gameObject);
        }
        CurrentTurn++;
        if (CurrentTurn >= m_encounter.Count)
        {
            CurrentTurn = 0;
        }
        currentTurnImage.transform.position = iniImages[CurrentTurn].transform.position;
        if (m_encounter[CurrentTurn].unit is Monsters)
        {
            List<initiative> aggro = m_encounter.Where(x => x.unit is PartyMembers).ToList();
            m_encounter[CurrentTurn].prefab.GetComponent<MnstrStats>().Attack(aggro);
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
        if (ability == null)
        {
            foreach (Transform mnstrs in GameObject.Find("monsterSpots").transform)
            {
                if (mnstrs.transform.childCount != 0)
                {
                    GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
                    newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mnstrs.GetChild(0).GetComponent<MnstrStats>()._name();
                    newBT.GetComponent<Button>().onClick.AddListener(delegate {
                        m_encounter[CurrentTurn].prefab.GetComponent<unitCombatStats>().Attack(mnstrs, BurnedAP);
                        attack();
                    });

                }
            }
        }
        //Received an ability
        else
        {
            //The target is a single enemy
            if (ability.Target.HasFlag(Abilities.ETarget.ENEMY))
            {
                List<Transform> targets = new List<Transform>();
                foreach (Transform mnstrs in GameObject.Find("monsterSpots").transform)
                {
                    if (mnstrs.transform.childCount != 0)
                    {
                        GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
                        newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mnstrs.GetChild(0).GetComponent<MnstrStats>()._name();
                        newBT.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            if (BurnedAP + ability.APCost <= AP)
                            {
                                RemoveAP(ability.APCost);
                                targets.Add(mnstrs.GetChild(0));
                                ability.Execute(targets, m_encounter[CurrentTurn].prefab.name);
                            }
                        });
                    }
                }
            }
            //the target is a single ally
            if (ability.Target.HasFlag(Abilities.ETarget.ALLY))
            {
                List<Transform> targets = new List<Transform>();
                int index = 0;
                foreach (Transform plyrs in GameObject.Find("spawnSpots").transform)
                {
                    if (plyrs.transform.childCount != 0)
                    {

                        GameObject newBT = Instantiate(BtPrefab, GameObject.Find("AbilityButtons").transform);
                        newBT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PartyManager.GetInstance().getParty()[index].memberName;
                        newBT.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            if (BurnedAP + ability.APCost <= AP)
                            {
                                RemoveAP(ability.APCost);
                                targets.Add(plyrs.GetChild(0));
                                ability.Execute(targets, m_encounter[CurrentTurn].prefab.name);
                            }
                        }
                        );
                    }
                    index++;
                }
            }
            //the target is the caster
            if (ability.Target.HasFlag(Abilities.ETarget.SELF))
            {
                List<Transform> targets = new List<Transform>();
                targets.Add(m_encounter[CurrentTurn].prefab.transform);
                ability.Execute(targets, m_encounter[CurrentTurn].prefab.name);
                RemoveAP(ability.APCost);

            }
            //the target is all allies
            if (ability.Target.HasFlag(Abilities.ETarget.AllyAll) || ability.Target.HasFlag(Abilities.ETarget.EnemyAll))
            {
                List<Transform> targets = new List<Transform>();
                if (ability.Target.HasFlag(Abilities.ETarget.AllyAll))
                {
                    foreach (Transform plyrs in GameObject.Find("spawnSpots").transform)
                    {
                        if (plyrs.transform.childCount != 0)
                        {
                            targets.Add(plyrs.GetChild(0));
                        }
                    }

                }
                //the target is all enemies
                if (ability.Target.HasFlag(Abilities.ETarget.EnemyAll))
                {
                    foreach (Transform mnstrs in GameObject.Find("monsterSpots").transform)
                    {
                        if (mnstrs.transform.childCount != 0)
                        {
                            targets.Add(mnstrs.GetChild(0));
                        }
                    }
                }
                if (ability.APCost <= AP)
                {
                    RemoveAP(ability.APCost);
                    ability.Execute(targets, m_encounter[CurrentTurn].prefab.name);
                }
            }
        }
    }

    public void RemoveAP(float apToRemove)
    {
        AP -= apToRemove;
        APGauge.GetComponent<ImgsFillDynamic>().SetValue(AP / MaxAp, false, 1);
    }

    private void attack()
    {
        
        if (BurnedAP == 0 && AP < MaxAp)
        {
            AP += 1;
            APGauge.GetComponent<ImgsFillDynamic>().SetValue(AP / MaxAp, false, 1);
        }
        BurnedAP = 0;
        burnText.gameObject.SetActive(false);
        endTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BurnAp();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnBurnAp();
        }
    }

    private void BurnAp()
    {
        BurnedAP += 1;
        burnText.gameObject.SetActive(true);
        if (BurnedAP > AP)
        {
            BurnedAP = AP;
        }
        burnText.text = "-" + BurnedAP;

    }

    private void UnBurnAp()
    {
        BurnedAP -= 1;
        if (BurnedAP <= 0)
        {
            burnText.gameObject.SetActive(false);
            BurnedAP = 0;
        }
        burnText.text = "-" + BurnedAP;

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
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, (ElapsedTime / TotalTime));
            yield return null;
        }
        if (SceneManager.GetActiveScene().name != "FightScene")
        {
            SceneManager.LoadScene("FightScene");
        }
        else
        {
            SceneManager.LoadScene("OverWorld");
        }

    }


}
