using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float _totalMoney = 0f;
    public float TotalMoney => _totalMoney;

    private int _maxHealth;
    public int MaxHealth => _maxHealth;
    
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    private float _moveSpeedModifier = 0f;
    private float _fireDelayModifier = 0f;

    public UnityEvent MoneyChangeEvent;
    public UnityEvent HealthChangeEvent;
    public UnityEvent PlayerDeathEvent;
    public UnityEvent StatChangeEvent;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeMoney(float amount)
    {
        _totalMoney += amount;
        MoneyChangeEvent.Invoke();
        // Debug.Log("Money: " + _totalMoney);
    }

    public void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        HealthChangeEvent.Invoke();
        // Debug.Log("Health: " + _currentHealth);
    }
    
    public void SetMaxHealth(int amount, bool heal)
    {
        _maxHealth = amount;
        
        if (heal)
        {
            _currentHealth = _maxHealth;
        }
        
        HealthChangeEvent.Invoke();
    }
    
    public void ChangeMoveSpeed(float amount)
    {
        _moveSpeedModifier += amount;
        StatChangeEvent.Invoke();
    }
    public void ChangeFireDelay(float amount)
    {
        _fireDelayModifier += amount;
        StatChangeEvent.Invoke();
    }
    
    public void PlayerDeath()
    {
        PlayerDeathEvent.Invoke();
    }
}