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
    private SaveData saveData;
    private void Start()
    {
        saveData = SaveSystem.load();
        LoadMap(testMap);
    }

    public void LoadMap(SuperMap map)
    {
        Vector2 spawnPoint;
        //D�truire l'ancienne Map
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
            if (superObj.m_TiledName.Contains("Borders"))
            {

               Collider2D collider = superObj.GetComponent<Collider2D>();
               collider.enabled = true;
                
            }
            if (superObj.m_Type.Contains("Spawn"))
            {
                if(saveData == null)
                {
                    spawnPoint = superObj.gameObject.transform.position;

                }
                else
                {
                    spawnPoint = saveData.Position;
                }
                    PartyManager.GetInstance().Spawn(spawnPoint);
            }
            
            
        }
    }
}
