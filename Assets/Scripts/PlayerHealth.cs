using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    public int MaxHealth => _maxHealth;
    
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    
    [SerializeField] private float _stunTime = 0.25f;
    [SerializeField] private float _iFrameDuration = 1f;
    
    private SpriteRenderer _sr;
    
    private bool _isInvincible = false;
    private bool _isDowned = false;
    public bool IsDowned => _isDowned;
    
    [SerializeField] private Transform _coinRef;
    [SerializeField] private Sprite _downedSprite;
    [SerializeField] private GameObject _deathScreen;
    
    public UnityEvent HealthChangeEvent;
    public UnityEvent PlayerDeathEvent;
    
    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        WaveSpawner.Instance.FinalWaveCompleteEvent.AddListener(SetDowned);
    }

    private void OnDisable()
    {
        WaveSpawner.Instance.FinalWaveCompleteEvent.RemoveListener(SetDowned);
    }

    public void TakeDamage(Vector2 force,int damage)
    {
        if (!_isInvincible)
        {
            SFXManager.Instance.PlayPlayerHurtSFX();
            
            // Knockback
            GetComponent<PlayerMovement>().Knockback(force, _stunTime);
            
            // Subtract Health
            _currentHealth -= damage;
            
            // Check for death, if not, add iFrames
            if (_currentHealth <= 0 && !_isDowned)
            {
                Death();
            }
            else if(_currentHealth <= 0 && _isDowned)
            {
                StopCoroutine(IFrames(_iFrameDuration));
                StartCoroutine(IFrames(_iFrameDuration));

                if (PlayerStats.Instance.TotalMoney > 0)
                {
                    Transform coin = Instantiate(_coinRef, transform.position, quaternion.identity);
                    coin.GetComponent<Coin>().Knockback();
                    if (coin.GetComponent<Coin>().Amount > PlayerStats.Instance.TotalMoney)
                    {
                        PlayerStats.Instance.ChangeMoney(-PlayerStats.Instance.TotalMoney);
                    }
                    else
                    {
                        PlayerStats.Instance.ChangeMoney(-coin.GetComponent<Coin>().Amount);
                    }
                    
                }
            }
            else
            {
                StopCoroutine(IFrames(_iFrameDuration));
                StartCoroutine(IFrames(_iFrameDuration));
            }
            
            HealthChangeEvent.Invoke();
        }
    }
    
    public void Heal(int amount)
    {
        SFXManager.Instance.PlayHealSFX();
        
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        
        HealthChangeEvent.Invoke();
    }
    
    private IEnumerator IFrames(float duration)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(duration);
        _sr.color = Color.white;
        _isInvincible = false;
    }
    
    private void Death()
    {
        //Instantiate(_ps, transform.position, Quaternion.identity);
        //int random = UnityEngine.Random.Range(0, 3);
        
        SFXManager.Instance.PlayGameOverSFX();
        
        _isDowned = true;
        _sr.sortingOrder = -1;
        _sr.sprite = _downedSprite;
        _sr.color = Color.white;
        
        transform.rotation = Quaternion.Euler(0,0,90);
        
        transform.GetChild(2).gameObject.SetActive(false);
        
        MusicManager.Instance.SwapTrack(false);
        
        PlayerDeathEvent.Invoke();
    }

    private void SetDowned()
    {
        _isDowned = true;
    }
}
