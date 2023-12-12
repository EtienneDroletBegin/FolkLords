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
    protected Monsters type;
    [SerializeField]
    private GameObject HPBar;

    protected int HP;
    protected UnityEngine.UI.Slider HPSlider;
    protected string monsterName;
    protected int Damage;
    protected int resistance = 0;
    public bool isDead;
  
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
        HP -= (Damage - resistance);
        HPSlider.value = HP;
        if(IsDead())
        {
            HP = 0;
            isDead = true;
            //Destroy(gameObject);
        }
    }
    virtual public void Attack(List<initiative> aggro)
    {
        if (!IsDead())
        {
            List<initiative> updatedAggro = aggro.OrderByDescending(X => X.prefab.GetComponent<unitCombatStats>().GetAggro()).ToList();
            print(updatedAggro[0].prefab.name);
            initiative target = updatedAggro[0];
            StartCoroutine("monsterAttack", target);

        }
        else
        {
            EncounterManager.GetInstance().endTurn();

        }
    }

    IEnumerator monsterAttack(initiative Target)
    {
        yield return new WaitForSeconds(1f);
        Target.prefab.GetComponent<unitCombatStats>().TakeDamage(Damage);
        yield return new WaitForSeconds(1f);
        if (GetComponentInChildren<DisarmEffect>())
        {
            Destroy(GetComponentInChildren<DisarmEffect>().gameObject);
        }
        if (GetComponentInChildren<EstocEffect>())
        {
            Destroy(GetComponentInChildren<EstocEffect>().gameObject);
        }
        EncounterManager.GetInstance().endTurn();

    }
    public bool IsDead()
    {
        return HP <= 0;
    }


    public string _name() { return monsterName; }


}
