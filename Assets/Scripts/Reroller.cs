using TMPro;
using UnityEngine;

public class Reroller : MonoBehaviour
{
    [SerializeField] private float _rerollPrice = 0.05f;
    private GameObject _itemTextbox;
    private TMP_Text _itemName;
    private TMP_Text _itemDescription;
    private TMP_Text _itemPrice;
    private bool _isOverlapping;
    private TextPopup _textPopupRef;

    private void Awake()
    {
        _textPopupRef = GameObject.Find("Text Popup").GetComponent<TextPopup>();
        _itemTextbox = GameObject.Find("Item Textbox");
        
        _itemName = _itemTextbox.transform.GetChild(0).GetComponent<TMP_Text>();
        _itemDescription = _itemTextbox.transform.GetChild(1).GetComponent<TMP_Text>();
        _itemPrice = _itemTextbox.transform.GetChild(2).GetComponent<TMP_Text>();
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
            _itemTextbox.transform.position = transform.position + new Vector3(-2, -3f, 0);

            _itemName.text = "- Reroll -";
            _itemDescription.text = "Don't like the items? I'll swap em out for ya";
            _itemPrice.text = "$" + _rerollPrice;
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
        if (PlayerStats.Instance.TotalMoney >= _rerollPrice)
        {
            Reroll();
        }
        else
        {
            _textPopupRef.StartFade();
        }
    }
    
    private void Reroll()
    {
        ShopPedestal[] tet = FindObjectsOfType<ShopPedestal>();
            
        foreach (var VARIABLE in tet)
        {
            VARIABLE.Reroll();
        }
        
        PlayerStats.Instance.ChangeMoney(-_rerollPrice);
    }
}
