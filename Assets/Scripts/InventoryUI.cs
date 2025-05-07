using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI hpPotionText;
    public TextMeshProUGUI manaPotionText;

    private void Update()
    {
        if (InventoryManager.instance != null)
        {
            keyText.text = InventoryManager.instance.keys.ToString();
            hpPotionText.text = InventoryManager.instance.hpPotions.ToString();
            manaPotionText.text = InventoryManager.instance.manaPotions.ToString();
        }
    }
}