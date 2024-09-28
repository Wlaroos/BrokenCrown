using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoneCaltrop : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _canDamage = false;
    private float _startDelay = .2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(Random.Range(-3,3), Random.Range(-3,3)), ForceMode2D.Impulse);
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EnemyHealth>() != null && _canDamage)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(Vector2.zero, 1);
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator StartDelay()
    {
        _canDamage = false;
        yield return new WaitForSeconds(_startDelay);
        _canDamage = true;
    }
}
