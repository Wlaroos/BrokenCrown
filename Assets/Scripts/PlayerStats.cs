using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private float _totalMoney = 0f;
	public float TotalMoney => _totalMoney;
    private float _moveSpeedModifier = 0f;
	private float _fireDelayModifier = 0f;
    
	public UnityEvent MoneyChangeEvent;
    
    public void AddMoney(float amount)
	{
		_totalMoney += amount;
		MoneyChangeEvent.Invoke();
        //Debug.Log("Money: " + _totalMoney);
    }
    
	public void SubtractMoney(float amount)
	{
		_totalMoney += amount;
		MoneyChangeEvent.Invoke();
		//Debug.Log("Money: " + _totalMoney);
	}
}
