using SuperTiled2Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SuperMap testMap;
    [SerializeField] private GameObject playerPrefab;
    private GameObject currentMap;

    private void Start()
    {
        LoadMap(testMap);
    }

    public void LoadMap(SuperMap map)
    {
        //Détruire l'ancienne Map
        if (currentMap != null)
        { 
            Destroy(currentMap);
        }
        map.gameObject.transform.localScale = new Vector2(2, 2);
        //Spawner la nouvelle map
        currentMap = Instantiate(map).gameObject;
        
        //Obtenir la liste de tous les objects de la map
        SuperObject[] objects = currentMap.GetComponentsInChildren<SuperObject>();
        foreach (SuperObject superObj in objects)
        {
            //Regarder le m_Type pour savoir quoi faire
            if (superObj.m_Type.Contains("Borders"))
            {
               Collider collider = superObj.GetComponent<Collider>();
               collider.enabled = true;
                
            }
            else if (superObj.m_Type.Contains("Spawn"))
            {
                Vector2 spawnPoint = superObj.gameObject.transform.position;
                PartyManager.GetInstance().Spawn(spawnPoint);
            }
        }
    }
}
