

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int health = 100;
    public int attackDamage = 20;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f; // Seconds between attacks

    private Transform target; // The target to move towards and attack
    private float lastAttackTime;

    void Start()
    {
        // Find the player or other target (replace "Player" with your target's tag)
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastAttackTime = -attackCooldown; // Allow immediate attack on start
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
            AttackTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the monster towards the target
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void AttackTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            // Perform the attack (e.g., deal damage to the target)
            Debug.Log("Monster attacked the target for " + attackDamage + " damage!"); // Replace with actual damage application

            // Example: If target has a Health component, you might call:
            // target.GetComponent<Health>().TakeDamage(attackDamage);

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Monster took " + damage + " damage! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Monster died!");
        Destroy(gameObject); // Destroy the monster GameObject
        // You can add more death effects here (e.g., animations, sound, loot)
    }

    // Optional: Draw attack range in the Scene view for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
