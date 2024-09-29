using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // Total money the player has
    public float TotalMoney { get; private set; } = 0f;
    
    // Tells the game if the player is on the shopping screen
    public bool IsShopping { get; private set; } = false;
    
    public List<GameObject> Items = new List<GameObject>();

    private float _moveSpeedModifier = 0f;
    private float _fireDelayModifier = 0f;

    // Events
    public UnityEvent MoneyChangeEvent;
    public UnityEvent StatChangeEvent;
    public UnityEvent ShopScreenChangeEvent;
    public UnityEvent FightScreenChangeEvent;
    
    private PlayerHealth _playerHealth;

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
        
        _playerHealth = GetComponent<PlayerHealth>();
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
        
        if (isShopping)
        {
            ShopScreenChangeEvent.Invoke();
        }
        else
        {
            FightScreenChangeEvent.Invoke();
        }
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