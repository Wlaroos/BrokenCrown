using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkyNeedle : BaseItem
{
    [SerializeField] private float _shotSpeedModifierAmount = 1.5f;
    
    protected override void ItemEffects()
    {
        PlayerStats.Instance.ChangeShotSpeed(_shotSpeedModifierAmount);
    }
    
    protected override void Upgrade()
    {
        ItemEffects();
        PlayerStats.Instance.RemoveItemFromPool(_name);
    }
}
