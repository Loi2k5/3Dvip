using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Thực hiện các hành động khi đối tượng chết, ví dụ như hủy đối tượng
        Destroy(gameObject);
    }
}
