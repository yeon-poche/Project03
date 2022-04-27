using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    [SerializeField] Slider _magicSlider = null;

    public void SetMaxHealth(int health)
    {
        _magicSlider.maxValue = health;
        _magicSlider.value = health;
    }

    public void SetHealth(int health)
    {
        _magicSlider.value = health;
    }
}
