using UnityEngine;
using static ItemType;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public int keys = 0;
    public int hpPotions = 0;
    public int manaPotions = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
                    PlayerStatusManager.instance.HealHP(20f);
                    return true;
                }
                break;
            case ItemType.MANA_POTION:
                if (manaPotions > 0)
                {
                    manaPotions--;
                    PlayerStatusManager.instance.RestoreMana(20f);
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