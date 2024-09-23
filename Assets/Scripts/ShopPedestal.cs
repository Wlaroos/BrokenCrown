using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPedestal : MonoBehaviour
{
     private List<GameObject> _itemList = new List<GameObject>();
     private GameObject[] _itemArray;
     private GameObject _item;

     private GameObject _itemTextbox;
     private TMP_Text _itemName;
     private TMP_Text _itemDescription;
     private TMP_Text _itemPrice;
     private Sprite _itemSprite;

     private int _randomNum;
     
     private void Awake()
     {
         _itemTextbox = GameObject.Find("Item Textbox");
         
         _itemArray = Resources.LoadAll<GameObject>("Items");
         
         foreach (var item in _itemArray)
         {
             _itemList.Add(item);
         }
         
         _randomNum = UnityEngine.Random.Range(0, _itemList.Count);
         
         _item = _itemList[_randomNum];
         _itemList.Remove(_item);
         
         _itemName = _itemTextbox.transform.GetChild(0).GetComponent<TMP_Text>();
         _itemDescription = _itemTextbox.transform.GetChild(1).GetComponent<TMP_Text>();
         _itemPrice = _itemTextbox.transform.GetChild(2).GetComponent<TMP_Text>();
         _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
             
         transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
         if(other.GetComponent<PlayerMovement>() != null)
         {
             _itemTextbox.SetActive(true);
             _itemTextbox.transform.position = transform.position + new Vector3(0, -3f, 0);

             _itemName.text = "- " + _item.GetComponent<BaseItem>().GetName() + " -";
             _itemDescription.text = _item.GetComponent<BaseItem>().GetDescription();
             _itemPrice.text = _item.GetComponent<BaseItem>().GetPrice().ToString("F2");
         }
     }
     
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.GetComponent<PlayerMovement>() != null)
         {
             _itemTextbox.SetActive(false);
         }
     }

     public void Reroll()
     {
         _randomNum = UnityEngine.Random.Range(0, _itemList.Count);
         _item = _itemList[_randomNum];
         
         _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
         transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;
         
         if (_itemList.Count > 1)
         {
             //_itemList.Remove(_item);
         }
     }
}
