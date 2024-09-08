using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    private float _currentHealth;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private readonly float _knockbackMult = 1.0f;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(Vector2 force,int damage)
    {
        // Knockback
        _rb.AddForce(force * _knockbackMult, ForceMode2D.Impulse);

        StopCoroutine(FlashRed());
        StartCoroutine(FlashRed());
        
        // Subtract Health
        _currentHealth -= damage;
        
        //int random = UnityEngine.Random.Range(0, 4);
        //AudioHelper.PlayClip2D(enemyHitSFX[random], 1);
        //healthBar.localScale = new Vector3((((float)_currentHealth / (float)_maxHealth) * 0.315f), healthBar.localScale.y, healthBar.localScale.z);

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private IEnumerator FlashRed()
    {
        _sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _sr.color = Color.white;
    }

    private void Death()
    {
        //Instantiate(_ps, transform.position, Quaternion.identity);
        //int random = UnityEngine.Random.Range(0, 3);
        //AudioHelper.PlayClip2D(enemyDeathSFX[random], 1);
        
        Destroy(gameObject);
    }
}
