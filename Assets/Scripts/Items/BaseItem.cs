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
		if (PlayerStats.Instance.Items.Exists(x => x.name == _name))
		{
			Debug.Log("SOMETHING IS HERE");
			PlayerStats.Instance.Items.Find(x => x.name == _name).GetComponent<BaseItem>().Upgrade();
			Destroy();
		}
		else
		{
			Debug.Log("NORMAL");
			transform.SetParent(_itemHolder);
			transform.position = _itemHolder.position;
		
			GetComponent<SpriteRenderer>().enabled = false;
			ItemEffects();
			_activated = true;
		
			PlayerStats.Instance.Items.Add(gameObject);
		}
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
