using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void StartCombat()
    {
        StartCoroutine("BeginFade");

    }


    IEnumerator BeginFade()
    {
        Color startColor = Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);
        float ElapsedTime = 0.0f;
        float TotalTime = 1f;


        while (ElapsedTime < TotalTime)
        {
            Debug.Log(ElapsedTime / TotalTime);
            ElapsedTime += Time.deltaTime;
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, (ElapsedTime/TotalTime));
            yield return null;
        }

        SceneManager.LoadScene("FightScene");
    }
}
