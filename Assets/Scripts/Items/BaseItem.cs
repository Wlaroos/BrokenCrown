using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
	[SerializeField] private PlayerStats _playerStats;
	
	private Transform _itemHolder;
	
	private Sprite _sprite;
	private string _name;
	private string _description;
	private float _price;

	private void Awake()
	{
		_playerStats = GameObject.FindObjectOfType<PlayerStats>();
		_itemHolder = _playerStats.transform.Find("Items");
	}
	
	public void Init(Sprite sprite, string name, string description, float price)
	{
		_sprite = sprite;
		_name = name;
		_description = description;
		_price = price;
	}
	
	public void Purchase()
	{
		_playerStats.SubtractMoney(_price);
		ItemEffects();
	}
	
	public virtual void ItemEffects()
	{
		
	}
	
	private void AttachToPlayer()
	{
		transform.SetParent(_itemHolder);
	}
	
	private void Destroy()
	{
		Destroy(gameObject);
	}
}
