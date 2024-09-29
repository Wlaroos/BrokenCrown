using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPedestal : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    private Sprite _closedSprite;
    
     private List<GameObject> _itemList = new List<GameObject>();
     private GameObject[] _itemArray;
     private GameObject _item;

     // Item textbox
     private GameObject _itemTextbox;
     private TMP_Text _itemName;
     private TMP_Text _itemDescription;
     private TMP_Text _itemPrice;
     private Sprite _itemSprite;

     private int _randomNum;

     private ParticleSystem _ps;
     private SpriteRenderer _sr;
     private BoxCollider2D _bc;

     private TextPopup _textPopupRef;
     
     private bool _isPurchased;
     private bool _isOverlapping;
     
     private void Awake()
     {
         _ps = transform.GetChild(1).GetComponent<ParticleSystem>();
         _sr = GetComponent<SpriteRenderer>();
         _bc = GetComponent<BoxCollider2D>();
         _closedSprite = _sr.sprite;
         
         _textPopupRef = GameObject.Find("Text Popup").GetComponent<TextPopup>();
         
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

     private void Start()
     {
         PlayerStats.Instance.ShopScreenChangeEvent.AddListener(RerollAfterPurchased);
     }

     private void OnDisable()
     {
         PlayerStats.Instance.ShopScreenChangeEvent.RemoveListener(RerollAfterPurchased);
     }

     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.E) && _isOverlapping)
         {
             CheckPurchase();
         }
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
         if(other.GetComponent<PlayerMovement>() != null)
         {
             _itemTextbox.SetActive(true);
             _isOverlapping = true;
             _itemTextbox.transform.position = transform.position + new Vector3(0, -3f, 0);

             _itemName.text = "- " + _item.GetComponent<BaseItem>().GetName() + " -";
             _itemDescription.text = _item.GetComponent<BaseItem>().GetDescription();
             _itemPrice.text ="$" + _item.GetComponent<BaseItem>().GetPrice().ToString("F2");
         }
     }
     
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.GetComponent<PlayerMovement>() != null)
         {
             _isOverlapping = false;
             _itemTextbox.SetActive(false);
         }
     }
     
     private void CheckPurchase()
     {
         if (PlayerStats.Instance.TotalMoney >= _item.GetComponent<BaseItem>().GetPrice() && !_isPurchased)
         {
             PurchaseItem();
         }
         else
         {
             _textPopupRef.StartFade();
         }
     }
     
     private void PurchaseItem()
     {
         PlayerStats.Instance.ChangeMoney(-_item.GetComponent<BaseItem>().GetPrice());

         GameObject item = Instantiate(_item, transform.position, Quaternion.identity);
         item.GetComponent<BaseItem>().Purchased();

         _isPurchased = true;
         
         _bc.enabled = false;
         
         _ps.Play();
         foreach (Transform child in _ps.transform)
         {
             child.GetComponent<ParticleSystem>().Play();
         }
         
         _sr.sprite = _openSprite;
         transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;

         Debug.Log("Purchased " + _item.GetComponent<BaseItem>().GetName());
     }

     private void RerollAfterPurchased()
     {
         if (_isPurchased)
         {
             _sr.sprite = _closedSprite;
             
             _randomNum = UnityEngine.Random.Range(0, _itemList.Count);
             _item = _itemList[_randomNum];

             _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
             transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;

             if (_itemList.Count > 1)
             {
                 _itemList.Remove(_item);
             }
         }
     }

     public void Reroll()
     {
         if (!_isPurchased)
         {
             _randomNum = UnityEngine.Random.Range(0, _itemList.Count);
             _item = _itemList[_randomNum];

             _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
             transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;

             if (_itemList.Count > 1)
             {
                 _itemList.Remove(_item);
             }
         }
     }
}
