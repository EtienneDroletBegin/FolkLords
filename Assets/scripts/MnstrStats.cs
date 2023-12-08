using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MnstrStats : MonoBehaviour
{
    [SerializeField]
    private Monsters type;
    [SerializeField]
    private GameObject HPBar;

    private int HP;
    private UnityEngine.UI.Slider HPSlider;
    private string monsterName;
  

    private void Start()
    {
        HPSlider = HPBar.GetComponent<UnityEngine.UI.Slider>();
        HP = type.BaseHP;
        monsterName = type.monsterName;
        HPSlider.maxValue = type.BaseHP;
        HPSlider.value = HP;
    }

    public void TakeDamage(int Damage)
    {
        HP -= Damage;
        HPSlider.value = HP;
    }

    public string _name() { return monsterName; }


}
