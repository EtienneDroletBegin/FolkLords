using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private GameObject initIMG;

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
        foreach (PartyMembers unit in PartyManager.GetInstance().getParty())
        {
            print(unit.memberName);
            initiative ini = new initiative();
            ini.unit = unit;
            ini.init = Random.Range(1, unit.spd);
            m_encounter.Add(ini);
        }
        m_encounter = m_encounter.OrderByDescending(x => x.init).ToList();
        //print(m_encounter.Count);

        initIMG = Resources.Load<GameObject>("initIMG");
        foreach (initiative image in m_encounter)
        {
            Image go = Instantiate(initIMG, GameObject.Find("initiative").transform).GetComponent<Image>();
            go.sprite = image.unit.ConvertTo<PartyMembers>().Portrait;
        }
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
