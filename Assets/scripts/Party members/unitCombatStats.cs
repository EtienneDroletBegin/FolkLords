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
    int aggro = 1;
    bool covered = false;
    ParticleSystem ps;

    void Start()
    {
        HP = linkedMember.maxHP;
        HPBar.maxValue = linkedMember.maxHP;
        HPBar.value = HP;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void AugmentAggro(int aggroGain)
    {
        aggro += aggroGain;
    }

    public int GetAggro() { return aggro; }

    public void Cover()
    {
        if (!covered) { covered = true; }
    }


    public void TakeDamage(int damage)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        HP -= damage;
        HPBar.value = HP;
        camShake.instance.shake(2f, 0.3f);
        ps.Play();
    }


    public void Heal(int heal)
    {
        HP += heal;
    }
}
