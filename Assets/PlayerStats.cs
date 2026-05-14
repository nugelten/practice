using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.IO; 

[System.Serializable]
public class PlayerSaveData
{
    public float health;
}

public class PlayerStats : MonoBehaviour
{
    [Header("Õàðàêòåðèñòèêè")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("²íòåðôåéñ")]
    public Slider healthBar;
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "player_stats.json");
        LoadHealth();
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                TakeDamage(20f);
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainMenu");
            }
            if (Keyboard.current.f5Key.wasPressedThisFrame)
            {
                SaveHealth();
            }
            if (Keyboard.current.f9Key.wasPressedThisFrame)
            {
                LoadHealth();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Ãðàâåöü ïîìåð!");
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Çáåðåæåííÿ âèäàëåíî ï³ñëÿ ñìåðò³.");
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveHealth() 
    {
        PlayerSaveData data = new PlayerSaveData();
        data.health = currentHealth;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Ãðó çáåðåæåíî! ÕÏ: " + currentHealth);
    }

    public void LoadHealth()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            currentHealth = data.health;
            Debug.Log("Ãðó çàâàíòàæåíî! ÕÏ: " + currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            Debug.Log("Ôàéë çáåðåæåííÿ íå çíàéäåíî. Ñòàðò ç ïîâíèì çäîðîâ'ÿì.");
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
    }
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.IO; 

[System.Serializable]
public class PlayerSaveData
{
    public float health;
}

public class PlayerStats : MonoBehaviour
{
    [Header("Õàðàêòåðèñòèêè")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("²íòåðôåéñ")]
    public Slider healthBar;
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "player_stats.json");
        LoadHealth();
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                TakeDamage(20f);
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainMenu");
            }
            if (Keyboard.current.f5Key.wasPressedThisFrame)
            {
                SaveHealth();
            }
            if (Keyboard.current.f9Key.wasPressedThisFrame)
            {
                LoadHealth();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Ãðàâåöü ïîìåð!");
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Çáåðåæåííÿ âèäàëåíî ï³ñëÿ ñìåðò³.");
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveHealth()
    {
        PlayerSaveData data = new PlayerSaveData();
        data.health = currentHealth;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Ãðó çáåðåæåíî! ÕÏ: " + currentHealth);
    }

    public void LoadHealth()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            currentHealth = data.health;
            Debug.Log("Ãðó çàâàíòàæåíî! ÕÏ: " + currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            Debug.Log("Ôàéë çáåðåæåííÿ íå çíàéäåíî. Ñòàðò ç ïîâíèì çäîðîâ'ÿì.");
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
    }
}