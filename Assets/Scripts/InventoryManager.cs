using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemType;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public int keys = 0;
    public int hpPotions = 0;
    public int manaPotions = 0;

    public float hpHealAmount = 20f;
    public float manaRestoreAmount = 20f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TutorialScene")
        {
            keys = 0;
            hpPotions = 0;
            manaPotions = 0;
        }
    }

    public void AddItem(ItemType type, int amount)
    {
        switch (type)
        {
            case ItemType.KEY:
                keys += amount;
                break;
            case ItemType.HP_POTION:
                hpPotions += amount;
                break;
            case ItemType.MANA_POTION:
                manaPotions += amount;
                break;
        }

        Debug.Log($"Picked up {amount} x {type}");
    }

    public bool UseItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.HP_POTION:
                if (hpPotions > 0)
                {
                    hpPotions--;
                    PlayerStatusManager.instance.HealHP(hpHealAmount);
                    return true;
                }
                break;
            case ItemType.MANA_POTION:
                if (manaPotions > 0)
                {
                    manaPotions--;
                    PlayerStatusManager.instance.RestoreMana(manaRestoreAmount);
                    return true;
                }
                break;
        }
        return false;
    }

    public bool HasKey()
    {
        return keys > 0;
    }

    public void UseKey()
    {
        if (keys > 0) keys--;
    }
}
