using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth = 500;
    [SerializeField] int _currentHealth = 500;

    [SerializeField] GameObject _visualToDisable = null;

    //[SerializeField] Slider _healthSlider = null;

    private void Start()
    {
        _currentHealth = _maxHealth;   
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            _visualToDisable.SetActive(false);
            Debug.Log(this.gameObject + " has died");
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        _currentHealth -= damage;

        // update slider
        
    }
}
