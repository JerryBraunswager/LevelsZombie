using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private readonly float _minHealth = 0;

    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(float health)
    {
        _currentHealth -= health;
        //Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

        if (_currentHealth <= _minHealth) 
        { 
            Destroy(gameObject);
        }
    }
}