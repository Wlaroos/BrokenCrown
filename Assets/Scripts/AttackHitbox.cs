using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private float _damage = 1f;
    
    private bool _canAttack = true;
    public bool CanAttack => _canAttack;
    
    private Vector3 mousePos;

    private SpriteRenderer _sr;
    private BoxCollider2D _bc;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
        _sr.enabled = false;
        _bc.enabled = false;
    }
    
    private void Update()
    {
        if (_canAttack)
        {
            Aim();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyHealth>() != false)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(_damage);
        }
    }

    private void Aim()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
    
    IEnumerator AttackCoroutine()
    {
        _canAttack = false;
        _sr.enabled = true;
        _bc.enabled = true;
        
        yield return new WaitForSeconds(0.2f);
        
        _sr.enabled = false;
        _bc.enabled = false;
        _canAttack = true;
        
        yield return null;
    }
}
