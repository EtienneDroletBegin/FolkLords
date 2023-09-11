using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;
using Unity.VisualScripting;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SuperMap Map;
    [SerializeField] private GameObject playerPrefab;
    private GameObject CurrentMap;

    private void Start()
    {
        LoadMap(Map);
    }
    public void LoadMap(SuperMap map)
    {
        if(CurrentMap != null)
        {
            Destroy(CurrentMap);
        }

        CurrentMap = Instantiate(map).gameObject;

        SuperObject[] objects = CurrentMap.GetComponentsInChildren<SuperObject>();
        foreach (SuperObject obj in objects)
        {
            if(obj.m_Type.Contains("Portal"))
            {
                Collider2D collider = obj.GetComponent<Collider2D>();
                collider.isTrigger = true;

                Portal portal = obj.AddComponent<Portal>();

                SuperCustomProperties props = obj.GetComponent<SuperCustomProperties>();
                if(props.TryGetCustomProperty("Destination", out CustomProperty prop))
                {
                    portal.SetDestination(prop.GetValueAsString());
                }
            }
            else if(obj.m_Type.Contains("Spawn"))
            {
                Instantiate(playerPrefab, obj.transform.position, Quaternion.identity);
            }
        }
    }
}
