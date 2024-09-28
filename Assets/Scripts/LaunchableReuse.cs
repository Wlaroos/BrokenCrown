using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchableReuse : MonoBehaviour
{
    [SerializeField] private float _knockbackRecieved = 15f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _createOnHit;
    [SerializeField] private int _createOnHitAmount = 1;
    
    private Rigidbody2D _rb;
    
    private bool _wasHit;
    private bool _isDestroyed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        ParticleSystem ps = Instantiate(_particle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        foreach (Transform child in ps.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        
        if(_createOnHit != null)
        {
            for (int i = 0; i < _createOnHitAmount; i++)
            {
                Instantiate(_createOnHit, transform.position, Quaternion.identity);
            }
        }
    }
}
