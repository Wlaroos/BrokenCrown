using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private ParticleSystem _ps;
    
    private bool _isDestroyed;

    private void Awake()
    {
        _ps = transform.GetChild(0).GetComponent<ParticleSystem>();
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
        
        Destroy(gameObject);
    }
}
