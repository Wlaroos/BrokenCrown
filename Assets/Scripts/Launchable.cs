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
    
    private bool _isDestroyed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBullets>() != null && !_isDestroyed)
        {
            other.GetComponent<PlayerBullets>().Destroy();
            Knockback(other.transform.right);
        }
        if (other.GetComponent<EnemyHealth>() != null && !_isDestroyed && _rb.velocity.magnitude > 3f)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(transform.right, _damage);
            Explode();
        }
    }
    
    private void Knockback(Vector3 dir)
    {
        // Knockback
        _rb.AddForce(dir * _knockbackRecieved, ForceMode2D.Impulse);
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
