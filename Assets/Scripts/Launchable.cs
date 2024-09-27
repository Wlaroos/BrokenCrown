using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchable : MonoBehaviour
{
    [SerializeField] private float _knockbackRecieved = 15f;
    [SerializeField] private int _damage = 1;
    
    private ParticleSystem _particle;
    private Rigidbody2D _rb;
    
    private bool _wasHit;
    private bool _isDestroyed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is a player bullet, destroy the bullet and apply knockback
        if (other.GetComponent<PlayerBullets>() != null && !_isDestroyed)
        {
            other.GetComponent<PlayerBullets>().Destroy();
            Knockback(other.transform.right);
        }
        
        // If the object is moving fast enough, deal damage to the enemy
        if (other.GetComponent<EnemyHealth>() != null && !_isDestroyed && _wasHit && other.GetComponent<EnemyHealth>().IsDowned == false && _rb.velocity.magnitude > 2f)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(transform.right, _damage);
            Explode();
        }
    }
    
    private void Knockback(Vector3 dir)
    {
        // Knockback
        _rb.AddForce(dir * _knockbackRecieved, ForceMode2D.Impulse);
        
        StopAllCoroutines();
        StartCoroutine(Hitbox());
    }    
    
    private IEnumerator Hitbox()
    {
        _wasHit = true;
        yield return new WaitForSeconds(1f);
        _wasHit = false;
    }
    
    private void Explode()
    {
        _isDestroyed = true;

        _particle.Play();
        
        foreach (Transform child in _particle.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        
        transform.DetachChildren();
        
        Destroy(gameObject);
    }
}
