using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthSlider = null;

    public void SetMaxHealth(int health)
    {
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        _healthSlider.value = health;
    }
}
