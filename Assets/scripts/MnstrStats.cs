using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int Damage;
  

    private void Start()
    {
        Damage = type.BaseAtk;
        HPSlider = HPBar.GetComponent<UnityEngine.UI.Slider>();
        HP = type.BaseHP;
        monsterName = type.monsterName;
        HPSlider.maxValue = type.BaseHP;
        HPSlider.value = HP;
    }

    public void ReduceDamage(int reduction)
    {
        Damage -= reduction;
    }
    public void TakeDamage(int Damage)
    {
        camShake.instance.shake(2f, 0.3f);
        GetComponent<Animator>().SetTrigger("takedamage");
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        HP -= Damage;
        HPSlider.value = HP;
    }
    public void Attack(List<initiative> aggro)
    {
        aggro.OrderBy(X => X.prefab.GetComponent<unitCombatStats>().GetAggro());
        initiative target = aggro[0];
        StartCoroutine("monsterAttack", target);
    }

    IEnumerator monsterAttack(initiative Target)
    {
        yield return new WaitForSeconds(1f);
        Target.prefab.GetComponent<unitCombatStats>().TakeDamage(Damage);
        yield return new WaitForSeconds(1f);
        EncounterManager.GetInstance().endTurn();

    }


    public string _name() { return monsterName; }


}
