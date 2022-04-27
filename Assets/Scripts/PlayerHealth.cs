using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth = 1000;

    private int _currentHealth; 

    void Start()
    {
        _currentHealth = _maxHealth;    
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
}
