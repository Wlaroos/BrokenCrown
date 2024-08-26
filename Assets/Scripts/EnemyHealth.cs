using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    private float _currentHealth;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
