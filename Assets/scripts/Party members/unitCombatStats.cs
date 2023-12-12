using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitCombatStats : MonoBehaviour
{
    [SerializeField] private PartyMembers linkedMember;
    [SerializeField] private Slider HPBar;

    int HP;
    int resistance = 0;
    int Damage;
    int magDamage;
    [SerializeField]int aggro = 1;
    bool covered = false;
    ParticleSystem ps;

    void Start()
    {
        Damage = linkedMember.physDMG;
        magDamage = linkedMember.magDMG;
        HP = linkedMember.maxHP;
        HPBar.maxValue = linkedMember.maxHP;
        HPBar.value = HP;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void AugmentAggro(int aggroGain)
    {
        aggro += aggroGain;
    }

    public void ResistanceGain(int resGain)
    {
        resistance += resGain;
    }

    public int getDMG()
    {
        return Damage;
    }

    public int getMagDMG()
    {
        return magDamage;
    }


    public void AttkGain(int attkGain)
    {
        Damage += attkGain;
    }

    public int GetAggro() { return aggro; }

    public void Cover()
    {
        if (!covered) { covered = true; }
    }


    public void TakeDamage(int damage)
    {
        if (!covered)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            HP -= (damage - resistance);
            HPBar.value = HP;
            camShake.instance.shake(2f, 0.3f);
            ps.Play();
            if (GetComponentInChildren<BraceEffect>())
            {
                Destroy(GetComponentInChildren<BraceEffect>().gameObject);
            }
        }
        else
        {
            GameObject.Find("Drifter").GetComponent<unitCombatStats>().TakeDamage(damage);
            covered = false;
        }

    }

    public void Attack(Transform trgt, float BurnedAP)
    {
        trgt.GetChild(0).GetComponent<MnstrStats>().TakeDamage(Damage);
        if (GetComponentInChildren<TrustNobodyEffect>())
        {
            Destroy(GetComponentInChildren<TrustNobodyEffect>().gameObject);
        }

        if (BurnedAP > 0)
        {
            for (int i = 0; i < BurnedAP; i++)
            {
                trgt.GetChild(0).GetComponent<MnstrStats>().TakeDamage(Damage);
                EncounterManager.GetInstance().RemoveAP(1);
            }
        }
        AugmentAggro(1);
    }

    public void Heal(int heal)
    {
        HP += heal;
    }
}
