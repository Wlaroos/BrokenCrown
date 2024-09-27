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
	private Sprite _sprite;

	private bool _activated;

	protected virtual void Awake()
	{
		_itemHolder = GameObject.FindWithTag("ItemHolder").transform;
		_name = this.name;
		_sprite = GetComponent<SpriteRenderer>().sprite;
	}
	
	public virtual void Purchased()
	{
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

	private void AttachToPlayer()
	{
		transform.SetParent(_itemHolder);
		GetComponent<SpriteRenderer>().enabled = false;
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
