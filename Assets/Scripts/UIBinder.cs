using UnityEngine;
using UnityEngine.UI;

public class UIBinder : MonoBehaviour
{
    public Slider manaSlider;
    public Slider hpSlider;

    void Start()
    {
        if (PlayerStatusManager.instance != null)
        {
            PlayerStatusManager.instance.manaBar = manaSlider;
            PlayerStatusManager.instance.hpBar = hpSlider;
        }
    }
}