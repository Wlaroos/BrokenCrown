﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyManager : MonoBehaviour
{
	[SerializeField] private Sprite[] _fontSprites;
	[SerializeField] private Image _digit01;
	[SerializeField] private Image _digit02;
	[SerializeField] private Image _digit03;
	
	private PlayerStats _playerStatsRef;
	
	private float _moneyAmount = 0.00f;
	private string _moneyString = "0.00";
	
	private void Awake()
	{
		_playerStatsRef = GameObject.FindObjectOfType<PlayerStats>();
	}
	
	private void OnEnable()
	{
		_playerStatsRef.MoneyChangeEvent.AddListener(MoneyUpdate);
	}
	
	private void OnDisable()
	{
		_playerStatsRef.MoneyChangeEvent.RemoveListener(MoneyUpdate);
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.X)) 
		{
			_moneyAmount += 0.01f;
			MoneyUpdate();
		}
	}
	
	private void MoneyUpdate()
	{
		_moneyAmount = _playerStatsRef.TotalMoney;
		var result = (Mathf.Round(_moneyAmount * 100)) / 100.0;
		_moneyString = result.ToString("F2");

		_digit01.sprite = _fontSprites[(int)Char.GetNumericValue(_moneyString[0])];
		_digit02.sprite = _fontSprites[(int)Char.GetNumericValue(_moneyString[2])];
		_digit03.sprite = _fontSprites[(int)Char.GetNumericValue(_moneyString[3])];
	}

}
