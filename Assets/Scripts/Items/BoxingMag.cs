using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingMag : BaseItem
{
    [SerializeField] private float _fireRateModifierAmount = 1.5f;
    
    protected override void ItemEffects()
    {
        PlayerStats.Instance.ChangeFireRate(_fireRateModifierAmount);
        PlayerStats.Instance.RemoveItemFromPool(_name);
    }
}
