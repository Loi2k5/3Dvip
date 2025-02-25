using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Animator animator;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        animator.SetTrigger("TakeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        // Tạm dừng trò chơi
        Time.timeScale = 0f;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
