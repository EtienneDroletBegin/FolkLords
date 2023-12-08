using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MnstrStats : MonoBehaviour
{
    [SerializeField]
    private Monsters type;

    private int HP;
    private string monsterName;
  

    private void Start()
    {
        HP = type.BaseHP;
        monsterName = type.monsterName;
    }

    public string _name() { return monsterName; }


}
