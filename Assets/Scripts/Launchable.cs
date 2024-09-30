using System;
using System.Collections;
using UnityEngine;

public class Launchable : MonoBehaviour
{
    [SerializeField] private float _knockbackRecieved = 15f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _createOnHit;
    [SerializeField] private int _createOnHitAmount = 1;
    
    private ParticleSystem _particle;
    private Rigidbody2D _rb;
    
    private bool _wasHit;
    private bool _isDestroyed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if (transform.childCount > 0)
        {
            _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
    }

    private void OnEnable()
    {
        PlayerStats.Instance.ShopScreenChangeEvent.AddListener(Destroy);
    }

    private void OnDisable()
    {
        PlayerStats.Instance.ShopScreenChangeEvent.RemoveListener(Destroy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is a player bullet, destroy the bullet and apply knockback
        if (other.gameObject.GetComponent<PlayerBullets>() != null && !_isDestroyed)
        {
            other.gameObject.GetComponent<PlayerBullets>().Destroy();
            Knockback(other.transform.right);
            SFXManager.Instance.PlayEnemyHitSFX();
        }
        
        // If the object is moving fast enough, deal damage to the enemy
        if (other.gameObject.GetComponent<EnemyHealth>() != null && !_isDestroyed && _wasHit && _rb.velocity.magnitude > 1f)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(transform.right, _damage);
            Explode();
            SFXManager.Instance.PlayDestructableSFX();
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
        yield return new WaitForSeconds(1.5f);
        _wasHit = false;
    }
    
    private void Explode()
    {
        if(_createOnHit != null)
        {
            for (int i = 0; i < _createOnHitAmount; i++)
            {
                Instantiate(_createOnHit, transform.position, Quaternion.identity);
            }
        }
        
        if (transform.childCount > 0)
        {
            _particle.Play();
        
            foreach (Transform child in _particle.transform)
            {
                child.GetComponent<ParticleSystem>().Play();
            }
            
            transform.DetachChildren();
        }
        
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
