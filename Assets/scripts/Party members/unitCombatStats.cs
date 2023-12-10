using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitCombatStats : MonoBehaviour
{
    [SerializeField] protected PartyMembers linkedMember;

    int HP;
    int resistance = 0;
    int aggro = 1;
    bool covered = false;

    void Start()
    {
        HP = linkedMember.maxHP;
    }

    public void AugmentAggro(int aggroGain)
    {
        aggro += aggroGain;
    }

    public void Cover()
    {
        if (!covered) { covered = true; }
    }


    public void TakeDamage(int damage)
    {
        HP -= damage;
    }


    public void Heal(int heal)
    {
        HP += heal;
    }
}
