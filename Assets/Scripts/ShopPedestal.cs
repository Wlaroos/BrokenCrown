using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopPedestal : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    private Sprite _closedSprite;
    

     private GameObject _item;
     public GameObject Item => _item;

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
     
     private ShopPedestal _otherShopPedestal;

     private void Awake()
     {
         _ps = transform.GetChild(1).GetComponent<ParticleSystem>();
         _sr = GetComponent<SpriteRenderer>();
         _bc = GetComponent<BoxCollider2D>();
         _closedSprite = _sr.sprite;
     }

     private void OnEnable()
     {
         _randomNum = Random.Range(0, PlayerStats.Instance.ItemList.Count);
         
         _item = PlayerStats.Instance.ItemList[_randomNum];
         
         _textPopupRef = GameObject.Find("Text Popup").GetComponent<TextPopup>();
         
         _itemTextbox = GameObject.Find("Item Textbox");
         
         _itemName = _itemTextbox.transform.GetChild(0).GetComponent<TMP_Text>();
         _itemDescription = _itemTextbox.transform.GetChild(1).GetComponent<TMP_Text>();
         _itemPrice = _itemTextbox.transform.GetChild(2).GetComponent<TMP_Text>();
         _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
             
         transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;
         
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
             SFXManager.Instance.PlayTextboxHoverSFX();
             
             _itemTextbox.SetActive(true);
             _isOverlapping = true;
             _itemTextbox.transform.position = transform.position + new Vector3(0, -3f, 0);
             
             UpdateTextBox();
         }
     }
     
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.GetComponent<PlayerMovement>() != null)
         {
             SFXManager.Instance.PlayTextboxHoverSFX();
             
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
             SFXManager.Instance.PlayNoMoneySFX();
         }
     }
     
     private void PurchaseItem()
     {
         PlayerStats.Instance.ChangeMoney(-_item.GetComponent<BaseItem>().GetPrice());

         GameObject item = Instantiate(_item, transform.position, Quaternion.identity);
         item.GetComponent<BaseItem>().Purchased();

         _sr.sprite = _openSprite;
         _isPurchased = true;
         _bc.enabled = false;
         
         _ps.Play();
         foreach (Transform child in _ps.transform)
         {
             child.GetComponent<ParticleSystem>().Play();
         }
         
         transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
         
         SFXManager.Instance.PlayDestructableSFX();
         SFXManager.Instance.PlayUpgradeSFX();
         
         Debug.Log("Purchased " + _item.GetComponent<BaseItem>().GetName());
     }

     private void RerollAfterPurchased()
     {
         if (_isPurchased)
         {
             _sr.sprite = _closedSprite;
             _bc.enabled = true;
             _isPurchased = false;
             
             Reroll();
         }
     }

     public void Reroll()
     {
         if (!_isPurchased)
         {
             _randomNum = Random.Range(0, PlayerStats.Instance.ItemList.Count);
             if(PlayerStats.Instance.ItemList[_randomNum].Equals(_item))
             {
                 Reroll();
                 return;
             }
             _item = PlayerStats.Instance.ItemList[_randomNum];

             _itemSprite = _item.GetComponent<SpriteRenderer>().sprite;
             transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemSprite;
             
             UpdateTextBox();
         }
     }

     private void UpdateTextBox()
     {
         if (_isOverlapping)
         {
             _itemName.text = "- " + _item.GetComponent<BaseItem>().GetName() + " -";
             _itemDescription.text = _item.GetComponent<BaseItem>().GetDescription();
             _itemPrice.text = "$" + _item.GetComponent<BaseItem>().GetPrice().ToString("F2");
         }
     }
}
