using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe : BaseItem
{
    [SerializeField] private float _movementSpeedAmount = 1f;
    
    protected override void ItemEffects()
    {
        PlayerStats.Instance.ChangeMoveSpeed(_movementSpeedAmount);
        //_description = "You've found a second shoe! You move even FASTER!";
        //PlayerStats.Instance.SetItemStats(_name, newDescription:_description);
    }
    
    protected override void Upgrade()
    {
        PlayerStats.Instance.ChangeMoveSpeed(_movementSpeedAmount);
        PlayerStats.Instance.RemoveItemFromPool(_name);
    }
}
