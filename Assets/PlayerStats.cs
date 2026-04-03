using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Обов'язково додаємо цей рядок!

public class PlayerStats : MonoBehaviour
{
    [Header("Характеристики")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Інтерфейс")]
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        // Перевіряємо, чи підключена клавіатура (щоб уникнути помилок)
        if (Keyboard.current != null)
        {
            // ТЕСТ: Натисни 'T', щоб отримати шкоду (нова система)
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                TakeDamage(20f);
            }

            // Повернення в меню на клавішу ESC (нова система)
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Гравець помер!");
        // Завантажуємо головне меню при смерті
        SceneManager.LoadScene("MainMenu");
    }
}