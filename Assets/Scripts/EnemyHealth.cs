using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private float _maxDownedHealth = 3;

    [SerializeField] private Transform _coinRef;

    [SerializeField] private Sprite[] _sprites;
	[SerializeField] private Sprite[] _deathSprites;
    
	[SerializeField] private GameObject _deathPs;
	
    private float _currentHealth;
    private float _currentDownHealth;
    private Rigidbody2D _rb;
	private CapsuleCollider2D _cc;
    private SpriteRenderer _sr;
	private Animator _anim;
	private int _spriteIndex;
    private readonly float _knockbackMult = 1.0f;

    private bool _isDowned = false;
    public bool IsDowned => _isDowned;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cc = GetComponent<CapsuleCollider2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _currentHealth = _maxHealth;
        _currentDownHealth = _maxDownedHealth;
	    _anim.SetBool("isMoving", true);
	    _spriteIndex = Random.Range(0, _sprites.Length);
        _sr.sprite = _sprites[_spriteIndex];
    }
    
    public void TakeDamage(Vector2 force,int damage)
    {
        if (!_isDowned)
        {
            // Knockback
            _rb.AddForce(force * _knockbackMult, ForceMode2D.Impulse);

            StopAllCoroutines();
            StartCoroutine(FlashRed());

            // Subtract Health
            _currentHealth -= damage;

            //int random = UnityEngine.Random.Range(0, 4);
            //AudioHelper.PlayClip2D(enemyHitSFX[random], 1);
            //healthBar.localScale = new Vector3((((float)_currentHealth / (float)_maxHealth) * 0.315f), healthBar.localScale.y, healthBar.localScale.z);

            if (_currentHealth <= 0)
            {
                Downed();
                WaveSpawner.Instance.EnemyDowned(gameObject);
            }
        }
        else
        {
            // Knockback
            _rb.AddForce(force * (_knockbackMult / 3), ForceMode2D.Impulse);
            
            // Subtract Health
            _currentDownHealth -= 1;

            CoinSpawn();
            
            if (_currentDownHealth <= 0)
            {
                Death();
            }
        }
    }
    
    // Shows enemy was hit
    private IEnumerator FlashRed()
    {
        _sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _sr.color = Color.white;
    }

    private void Downed()
    {
        StopAllCoroutines();
        
	    _isDowned = true;
	    _sr.color = Color.white;
	    _sr.sprite = _deathSprites[_spriteIndex];
        transform.rotation = Quaternion.Euler(0,0,90);
        _anim.SetBool("isMoving", false);
    }
    
    private void CoinSpawn()
	{
		// Weighted random for how many coins spawn
		const int oneChance = 70;
		const int twoChance = 20;
		const int threeChance = 10;
        
		int value = 0;
        
		float x = Random.Range(0, oneChance + twoChance + threeChance);

		switch (x)
		{
			// One Coin
			case < oneChance:
				value = 1;
				break;
			// Two Coins
			case < oneChance + twoChance:
				value = 2;
				break;
			// Three Coins
			case < oneChance + twoChance + threeChance:
				value = 3;
				break;
		}

		// Spawn i amount of coins
		for (int i = 1; i <= value; i++)
		{
			Transform coin = Instantiate(_coinRef, transform.position, quaternion.identity);
			coin.GetComponent<Coin>().Knockback();
		}
	}
    
    private void Death()
    {
	    Instantiate(_deathPs, transform.position, Quaternion.identity);
        _cc.enabled = false;
	    StartCoroutine(StaticCoroutines.Fade(0.5f, _sr, Destroy));
	    
	    //int random = UnityEngine.Random.Range(0, 3);
	    //AudioHelper.PlayClip2D(enemyDeathSFX[random], 1);
    }
    
	private void Destroy()
	{
		Destroy(gameObject);
	}
}
