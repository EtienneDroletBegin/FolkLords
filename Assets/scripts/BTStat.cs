using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTStat : MonoBehaviour
{
    public Abilities linkedAbility;

    public Abilities GetAbilities() { return linkedAbility; }
    public void SetAbilities(Abilities ability) { linkedAbility = ability; }
}
