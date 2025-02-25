using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public void Start()
    {
        currentHP = maxHP;
    }
    public virtual void TakeDamege(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

    }

    internal void TakeDamage(int damageAmount)
    {
        throw new NotImplementedException();
    }
}
