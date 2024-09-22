using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // Total money the player has
    public float TotalMoney { get; private set; } = 0f;

    // Max health of the player
    public int MaxHealth { get; private set; } = 3;

    // Current health of the player
    public int CurrentHealth { get; private set; } = 3;
    
    // Tells the game if the player is on the shopping screen
    public bool IsShopping { get; private set; } = false;

    private float _moveSpeedModifier = 0f;
    private float _fireDelayModifier = 0f;

    // Events
    public UnityEvent MoneyChangeEvent;
    public UnityEvent StatChangeEvent;
    public UnityEvent ScreenChangeEvent;

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
        TotalMoney += amount;
        MoneyChangeEvent.Invoke();
        // Debug.Log("Money: " + _totalMoney);
    }
    
    public void ScreenChange(bool isShopping)
    {
        IsShopping = isShopping;
        ScreenChangeEvent.Invoke();
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
    
}