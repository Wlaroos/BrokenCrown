using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public UnityEvent HealthChangeEvent;
    public UnityEvent PlayerDeathEvent;
    
    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _currentHealth = _maxHealth;
        
    }
    
    public void TakeDamage(Vector2 force,int damage)
    {
        if (!_isInvincible)
        {
            // Knockback
            GetComponent<PlayerMovement>().Knockback(force, _stunTime);
            
            // Subtract Health
            _currentHealth -= damage;
            
            // Check for death, if not, add iFrames
            if (_currentHealth <= 0)
            {
                Death();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(IFrames(_iFrameDuration));
            }
            
            HealthChangeEvent.Invoke();
        }

        //int random = UnityEngine.Random.Range(0, 4);
        //AudioHelper.PlayClip2D(enemyHitSFX[random], 1);
        //healthBar.localScale = new Vector3((((float)_currentHealth / (float)_maxHealth) * 0.315f), healthBar.localScale.y, healthBar.localScale.z);
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
        //AudioHelper.PlayClip2D(enemyDeathSFX[random], 1);
        
        _sr.color = Color.white;
        Debug.Log("Dead");
        //Destroy(gameObject);
        
        PlayerDeathEvent.Invoke();
    }
}
