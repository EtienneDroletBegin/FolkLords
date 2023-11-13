using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float health { get;set; }
    public void TakeDamage(float _amount);
    public void Heal(float _amount);
    public void Kill();
    public void FullHeal();
}
