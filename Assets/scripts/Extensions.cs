using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static GameObject ResetPosition(this GameObject _gameObject)
    {
        _gameObject.transform.position = Vector3.zero;
        return _gameObject;
    }
   
    public static T GetRandom<T>(this List<T> list)
    {
        int random = Random.Range(0, list.Count);
        return list[random];
    }

    public static Transform ClearChidlren(this Transform _transform)
    {
        for(int i = _transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(_transform.GetChild(i).gameObject);
        }
        return _transform;
    }

    //public static GameObject MoveAndAct(this GameObject _gameObject)
    //{
        
    //}

}
