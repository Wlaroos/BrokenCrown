using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietSchmoozeCat : BaseItem
{
    [SerializeField] private float _rangeModifierAmount = 1.33f;
    
    protected override void ItemEffects()
    {
        PlayerStats.Instance.ChangeRange(_rangeModifierAmount);
    }
    
    protected override void Upgrade()
    {
        ItemEffects();
        PlayerStats.Instance.RemoveItemFromPool(_name);
    }
}
