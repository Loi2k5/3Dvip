using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Animator animator;
    public Slider healthSlider;
    public GameObject deathPanel; // Thêm biến cho Panel chết

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        deathPanel.SetActive(false); // Ẩn panel chết khi bắt đầu
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
        deathPanel.SetActive(true); // Hiển thị panel chết
        Time.timeScale = 0f; // Dừng game
    }

    public void QuitGame()
    {
        Application.Quit(); // Thoát game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng trong Editor
#endif
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
