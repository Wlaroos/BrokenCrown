using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
	private Transform _itemHolder;
	
	[SerializeField] protected string _name;
	[SerializeField] protected string _description;
	[SerializeField] protected float _price;

	private bool _activated;

	protected virtual void Awake()
	{
		_itemHolder = GameObject.FindWithTag("ItemHolder").transform;
		_name = this.name;
	}
	
	public virtual void Purchased()
	{
		AttachToPlayer();
	}

	protected void Update()
	{
		if (_activated)
		{
			ItemEffectsUpdate();
		}
	}

	protected virtual void ItemEffectsUpdate()
	{
		
	}
	
	protected virtual void ItemEffects()
	{
		
	}
	
	protected virtual void Upgrade()
	{

	}

	private void AttachToPlayer()
	{
		if (PlayerStats.Instance.EquippedItems.Exists(x => x.name == _name))
		{
			PlayerStats.Instance.EquippedItems.Find(x => x.name == _name).GetComponent<BaseItem>().Upgrade();
			Destroy();
		}
		else
		{
			transform.SetParent(_itemHolder);
			transform.position = _itemHolder.position;
		
			GetComponent<SpriteRenderer>().enabled = false;
			ItemEffects();
			_activated = true;
		
			PlayerStats.Instance.EquippedItems.Add(gameObject);
		}
	}

	private void Destroy()
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
	
	public float GetPrice()
	{
		return _price;
	}
	
	public void SetName(string name)
	{
		_name = name;
	}
	
	public void SetDescription( string description)
	{
		_description = description;
	}
	
	public void SetPrice(float price)
	{
		_price = price;
	}
}
