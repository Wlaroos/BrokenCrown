using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCan : MonoBehaviour
{
    private ParticleSystem _ps;

    private void Awake()
    {
        if (transform.childCount > 0)
        {
            _ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(transform.right,1);
            OnDestroy();
        }
        else if (other.GetComponent<PlayerBullets>() != null)
        {
            SFXManager.Instance.PlayEnemyHitSFX();
            Destroy(other.gameObject);
            OnDestroy();
        }
    }
    
    private void OnDestroy()
    {
        if (transform.childCount > 0)
        {
            _ps.Play();
        
            foreach (Transform child in _ps.transform)
            {
                child.GetComponent<ParticleSystem>().Play();
            }
            
            transform.DetachChildren();
        }
        Destroy(gameObject);
    }
}
