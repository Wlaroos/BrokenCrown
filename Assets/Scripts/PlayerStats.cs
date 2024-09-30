using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // Total money the player has
    public float TotalMoney { get; private set; }
    
    // Tells the game if the player is on the shopping screen
    public bool IsShopping { get; private set; }
    
    public List<GameObject> EquippedItems;
    
    public float MoveSpeedModifier { get; private set; } = 1f;
    
    public float FireRateModifier { get; private set; } = 1f;

    public float RangeModifier { get; private set; } = 1f;
    
    public float ShotSpeedModifier { get; private set; } = 1f;
    
    // Events
    public UnityEvent MoneyChangeEvent;
    public UnityEvent StatChangeEvent;
    public UnityEvent ShopScreenChangeEvent;
    public UnityEvent FightScreenChangeEvent;
    
    public List<GameObject> ItemList { get; } = new List<GameObject>();

    private GameObject[] _itemArray;

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
        
        _itemArray = Resources.LoadAll<GameObject>("Items");
         
        foreach (var item in _itemArray)
        {
            ItemList.Add(item);
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
        
        if (isShopping)
        {
            ShopScreenChangeEvent.Invoke();
        }
        else
        {
            FightScreenChangeEvent.Invoke();
        }
    }

    public void RemoveItemFromPool(string name)
    {
        ItemList.Remove(ItemList.Find(x => x.name == name.Replace("(Clone)", "")));
    }
    
    public void SetItemStats(string name, string newName = "", string newDescription = "")
    {
        BaseItem item = ItemList.Find(x => x.name == name.Replace("(Clone)", "")).GetComponent<BaseItem>();
        
        if(newName != "")
        {
            item.SetName(newName);
        }
        if(newDescription != "")
        {
            item.SetDescription(newDescription);
        }
    }
    
    public void ChangeMoveSpeed(float amount)
    {
        MoveSpeedModifier += amount;
        StatChangeEvent.Invoke();
    }
    
    public void ChangeFireRate(float amount)
    {
        FireRateModifier *= amount;
        StatChangeEvent.Invoke();
    }
    
    public void ChangeRange(float amount)
    {
        RangeModifier *= amount;
        StatChangeEvent.Invoke();
    }

    public void ChangeShotSpeed(float amount)
    {
        ShotSpeedModifier *= amount;
        StatChangeEvent.Invoke();
    }
}