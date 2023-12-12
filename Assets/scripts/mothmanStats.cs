using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class mothmanStats : MnstrStats
{

    private bool hasAttacked;
    private bool hasHealed;
    private int abilityIndex = 0;

    public override void Attack(List<initiative> aggro)
    {
        List<initiative> updatedAggro = aggro.OrderByDescending(X => X.prefab.GetComponent<unitCombatStats>().GetAggro()).ToList();
        initiative target = updatedAggro[0];

        if (!hasAttacked)
        {
            StartCoroutine("monsterAttack", target);
            hasAttacked = true;
        }
        else
        {
            StartCoroutine("ability", target);
        }
    }


    IEnumerator ability(initiative target)
    {
        yield return new WaitForSeconds(1f);
        if (HP <= type.BaseHP / 2 && !hasHealed)
        {
            List<Transform> targets = new List<Transform>();
            targets.Add(transform);
            type.moves[2].Execute(targets, "Mothman");
        }
        else
        {
            
            Abilities ability = type.moves[abilityIndex];
            if (ability.Target.HasFlag(Abilities.ETarget.AllyAll))
            {
                List<Transform> targets = new List<Transform>();
                foreach (Transform plyrs in GameObject.Find("spawnSpots").transform)
                {
                    targets.Add(plyrs.GetChild(0));
                }
                ability.Execute(targets, "Mothman");
            }
            if (ability.Target.HasFlag(Abilities.ETarget.ALLY))
            {
                List<Transform> targets = new List<Transform>();
                targets.Add(target.prefab.transform);
                ability.Execute(targets, "Mothman");
            }

            abilityIndex++;
            if (abilityIndex > 1)
            {
                abilityIndex = 0;
            }
        }
        yield return new WaitForSeconds(1);

        EncounterManager.GetInstance().endTurn();
    }
}
