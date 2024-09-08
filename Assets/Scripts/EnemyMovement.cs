using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerRef;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _force = 2f;
    
    private BoxCollider2D _bc;
    private Rigidbody2D _rb;
    
    private const float ForcePower = 10f;
    
    private Vector2 direction;
    
    private void Awake()
    {
        _playerRef = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            other.GetComponent<PlayerHealth>().TakeDamage(directionTowardsTarget,1);
        }
    }

    private void Update() 
    {
        var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
        MoveTo(directionTowardsTarget);
    }
    
    private void FixedUpdate() 
    {
        var desiredVelocity = direction * _speed;
        var deltaVelocity = desiredVelocity - _rb.velocity;
        Vector3 moveForce = deltaVelocity * (_force * ForcePower * Time.fixedDeltaTime);
        _rb.AddForce(moveForce);
    }
    
    private void MoveTo (Vector2 direction) 
    {
        this.direction = direction;
    }

    private void Stop() 
    {
        MoveTo(Vector2.zero);
    }
    
    
}
