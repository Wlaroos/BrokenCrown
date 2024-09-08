using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _totalMoney = 0f;
    private float _moveSpeedModifier = 0f;
    private float _fireDelayModifier = 0f;
    
    public void AddMoney(float amount)
    {
        _totalMoney += amount;
        //Debug.Log("Money: " + _totalMoney);
    }
}
