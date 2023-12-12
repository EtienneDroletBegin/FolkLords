using SuperTiled2Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SuperMap testMap;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Image[] BattleBackgrounds;
    [SerializeField] private GameObject InteractionZone;
    [SerializeField] private List<ScriptableObject> Encounters;

    
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
        //Détruire l'ancienne Map
        if (currentMap != null)
        { 
            Destroy(currentMap);
        }
        map.gameObject.transform.localScale = new Vector2(2, 2);
        //Spawner la nouvelle map
        currentMap = Instantiate(map).gameObject;
        currentMap.GetComponentInChildren<Grid>().AddComponent<TilemapCollider2D>();
        

        //Obtenir la liste de tous les objects de la map
        SuperObject[] objects = currentMap.GetComponentsInChildren<SuperObject>();
       
        foreach (SuperObject superObj in objects)
        {
            //Regarder le m_Type pour savoir quoi faire
            if (superObj.m_Type.Contains("Encounter"))
            {
                Doors door = Instantiate(InteractionZone, superObj.transform.position, Quaternion.identity).GetComponent<Doors>();
                Debug.Log(int.Parse(superObj.m_TiledName));
                Encounters encounter = Encounters.ElementAt(int.Parse(superObj.m_TiledName)).ConvertTo<Encounters>();
                Monsters[] mnstr = encounter.monsters;
                Sprite bg = encounter.background;
                door.SetEncounterMonsters(mnstr);
                door.SetBackground(bg);
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
