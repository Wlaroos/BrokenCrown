using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descructible : MonoBehaviour
{
    private ParticleSystem _particle;
    
    private bool _isDestroyed;

    private void Awake()
    {
        _particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBullets>() != null && !_isDestroyed)
        {
            Explode();
        }
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
