using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField] private List<Sprite> _coinSprite = new List<Sprite>();
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
        if (other.GetComponent<PlayerStats>() != null)
        {
            other.GetComponent<PlayerStats>().AddMoney(_amount);
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
        const int pennyChance = 100;
        const int nickleChance = 20;
        const int dimeChance = 10;
        const int quarterChance = 5;
        
        int value = 0;
        
        float x = Random.Range(0, pennyChance + nickleChance + dimeChance + quarterChance);

        switch (x)
        {
            case < pennyChance:
                value = 1;
                break;
            case < pennyChance + nickleChance:
                value = 2;
                break;
            case < pennyChance + nickleChance + dimeChance:
                value = 3;
                break;
            default:
                value = 4;
                break;
        }

        switch (value)
        {
            case 1:
                _amount = 0.01f;
                _sr.sprite = _coinSprite[0];
                break;
            case 2:
                _amount = 0.05f;
                _sr.sprite = _coinSprite[1];
                break;
            case 3:
                _amount = 0.10f;
                _sr.sprite = _coinSprite[2];
                break;
            case 4:
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
