using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField] private List<Sprite> _coinSprite = new List<Sprite>();
    [SerializeField] private Transform _coinPopup;
    private float _amount;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private SpriteRenderer _sr;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        
        CoinSetup();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // This is done so that the coins can interact with the walls and each other
        // The player picks them up via their 'CoinTrigger' child that is on the Coin layer instead of the Player layer
        if (other.name == "CoinTrigger")
        {
            // PlayerStats in is parent object as stated above
            PlayerStats.Instance.ChangeMoney(_amount);

            // Create the coin popup and have is spawn slightly above the player
            var position = other.transform.position;
            Transform coinPop = Instantiate(_coinPopup, new Vector3(position.x, position.y + 1.33f, position.z), Quaternion.identity);
            
            // Round the amount to 2 decimal places
            var result = (Mathf.Round(_amount * 100)) / 100.0;
            coinPop.GetComponent<TMP_Text>().text = result.ToString("F2");
            
            Remove();
        }
    }

    public void Knockback()
    {
        float random = Random.Range(0f, 360f);
        
        transform.rotation = (Quaternion.Euler(0,0,random));

        _rb.drag = Random.Range(5,15);
        _rb.AddForce(new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * Random.Range(5,20), ForceMode2D.Impulse);
    }
    
    private void CoinSetup()
    {
        
        // Weighted random chance for coin amounts
        const int pennyChance = 85;
        const int nickleChance = 10;
        const int dimeChance = 4;
        const int quarterChance = 1;
        
        float x = Random.Range(0, pennyChance + nickleChance + dimeChance + quarterChance);

        // Switch case for which coin gets chosen
        switch (x)
        {
            // Penny
            case < pennyChance:
                _amount = 0.01f;
                _sr.sprite = _coinSprite[0];
                break;
            // Nickle
            case < pennyChance + nickleChance:
                _amount = 0.05f;
                _sr.sprite = _coinSprite[1];
                break;
            // Dime
            case < pennyChance + nickleChance + dimeChance:
                _amount = 0.10f;
                _sr.sprite = _coinSprite[2];
                break;
            // Quarter
            default:
                _amount = 0.25f;
                _sr.sprite = _coinSprite[3];
                break;
        }
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
