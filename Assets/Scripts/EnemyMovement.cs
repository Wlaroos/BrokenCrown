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
    private SpriteRenderer _sr;
    
    private const float ForcePower = 10f;
    
    private Vector2 direction;
    
    private void Awake()
    {
        _playerRef = GameObject.Find("Player").transform;
        _bc = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    /*
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            other.GetComponent<PlayerHealth>().TakeDamage(directionTowardsTarget,1);
        }
    }
    */
    
    // Works if enemy is already inside the hitbox
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            other.GetComponent<PlayerHealth>().TakeDamage(directionTowardsTarget,1);
        }
    }

    private void Update() 
    {
        // Make enemy look at player
        var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
        _sr.flipX = directionTowardsTarget.x < 0;
        
        // Sets direction of the enemy
        MoveTo(directionTowardsTarget);
    }
    
    private void FixedUpdate() 
    {
        // Movement
        var desiredVelocity = direction * _speed;
        var deltaVelocity = desiredVelocity - _rb.velocity;
        Vector3 moveForce = deltaVelocity * (_force * ForcePower * Time.fixedDeltaTime);
        _rb.AddForce(moveForce);
    }
    
    // Sets direction of the enemy
    private void MoveTo (Vector2 direction) 
    {
        this.direction = direction;
    }

    
    private void Stop() 
    {
        MoveTo(Vector2.zero);
    }
    
    
}
