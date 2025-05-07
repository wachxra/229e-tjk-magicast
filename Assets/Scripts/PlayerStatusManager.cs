using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager instance;

    public float maxMana = 50f;
    public float currentMana = 50f;

    public float maxHP = 100f;
    public float currentHP = 100f;

    public Slider manaBar;
    public Slider hpBar;

    [Header("Scene Persistence")]
    public bool isPersistentInstance = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ResetStatus();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetStatus()
    {
        currentHP = maxHP;
        currentMana = maxMana;
    }

    private void Update()
    {
        if (manaBar != null)
            manaBar.value = currentMana / maxMana;

        if (hpBar != null)
            hpBar.value = currentHP / maxHP;

        if (currentHP <= 0)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                player.HandleDeath();
            }
        }
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    public void RestoreMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
    }

    public void TakeDamage(float amount)
    {
        currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
    }

    public void HealHP(float amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }
}