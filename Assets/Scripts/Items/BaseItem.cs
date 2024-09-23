using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
	protected Transform _itemHolder;
	
	[SerializeField] protected string _name;
	[SerializeField] protected string _description;
	[SerializeField] protected float _price;
	protected Sprite _sprite;
	
	protected bool _activated;

	protected virtual void Awake()
	{
		_itemHolder = GameObject.Find("ItemHolder").transform;
		_name = this.name;
		_sprite = GetComponent<SpriteRenderer>().sprite;
	}
	
	protected virtual void Purchase()
	{
		PlayerStats.Instance.ChangeMoney(-_price);
		AttachToPlayer();
	}

	protected void Update()
	{
		if (_activated)
		{
			ItemEffects();
		}
	}

	protected virtual void ItemEffects()
	{
		
	}
	
	protected void AttachToPlayer()
	{
		transform.SetParent(_itemHolder);
		_activated = true;
	}
	
	protected void Destroy()
	{
		Destroy(gameObject);
	}
	
	public string GetName()
	{
		return _name;
	}
	
	public string GetDescription()
	{
		return _description;
	}
	
	public Sprite GetSprite()
	{
		return _sprite;
	}
	
	public float GetPrice()
	{
		return _price;
	}
}
