using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AttackHitbox _ah;
    
    private void Awake()
    {
        _ah = GetComponentInChildren<AttackHitbox>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _ah.CanAttack)
        {
            _ah.Attack();
        }
    }
}
