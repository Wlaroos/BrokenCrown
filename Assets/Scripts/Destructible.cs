using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Coin _coinRef;
    [SerializeField] private HealthItem _healthItemRef;
    
    private ParticleSystem _ps;
    private PlayerHealth _playerHealth;
    
    private bool _isDestroyed;

    private void Awake()
    {
        _ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBullets>() != null && !_isDestroyed)
        {
            //other.GetComponent<PlayerBullets>().Destroy();
            Explode();
        }
    }
    
    private void Explode()
    {
        _isDestroyed = true;

        _ps.Play();
        foreach (Transform child in _ps.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        
        transform.DetachChildren();
        
        CoinSpawn();
        HealthSpawn();
        
        Destroy(gameObject);
    }

    private void CoinSpawn()
    {
        // Weighted random for how many coins spawn
        const int zeroChance = 60;
        const int oneChance = 30;
        const int twoChance = 10;
        
        int value = 0;
        
        float x = Random.Range(0, zeroChance + oneChance + twoChance);

        switch (x)
        {
            // Zero Coins
            case < zeroChance:
                value = 0;
                break;
            // One Coin
            case < zeroChance + oneChance:
                value = 1;
                break;
            // Two Coins
            case < zeroChance + oneChance + twoChance:
                value = 2;
                break;
        }

        // Spawn i amount of coins
        for (int i = 1; i <= value; i++)
        {
            Coin coin = Instantiate(_coinRef, transform.position, quaternion.identity);
            coin.Knockback();
        }
    }
    
    private void HealthSpawn()
    {
        int random = Random.Range(0, 100);
        
        if (_playerHealth.CurrentHealth < _playerHealth.MaxHealth && random < 5)
        { 
            Instantiate(_healthItemRef, transform.position, quaternion.identity);
        }
    }
}
