using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _force = 5f;
    [SerializeField] private GameObject _warningPrefab;
    [SerializeField] private float _startDelay = 1f;
    
    private Transform _playerRef;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private EnemyHealth _eh;
    
    private const float ForcePower = 10f;
    
    private Vector2 direction;
    
    private bool _canMove = false;
    
    private void Awake()
    {
        _playerRef = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _eh = GetComponent<EnemyHealth>();
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position,_playerRef.position - transform.position, 100f, LayerMask.GetMask("RaycastLayer"));
        Instantiate(_warningPrefab, hit.point, Quaternion.identity);
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    // Works if enemy is already inside the hitbox
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null && !_eh.IsDowned)
        {
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            other.GetComponent<PlayerHealth>().TakeDamage(directionTowardsTarget,1);
        }
    }

    private void Update() 
    {
        if (!_eh.IsDowned && _canMove)
        {
            // Make enemy look at player
            var directionTowardsTarget = (_playerRef.position - this.transform.position).normalized;
            _sr.flipX = directionTowardsTarget.x < 0;

            // Sets direction of the enemy
            MoveTo(directionTowardsTarget);
        }
    }
    
    private void FixedUpdate() 
    {
        if (!_eh.IsDowned && _canMove)
        {
            // Movement
            var desiredVelocity = direction * _speed;
            var deltaVelocity = desiredVelocity - _rb.velocity;
            Vector3 moveForce = deltaVelocity * (_force * ForcePower * Time.fixedDeltaTime);
            _rb.AddForce(moveForce);
        }
    }
    
    // Sets direction of the enemy
    private void MoveTo (Vector2 direction) 
    {
        this.direction = direction;
    }
    
    private IEnumerator StartDelay()
    {
        _canMove = false;
        yield return new WaitForSeconds(_startDelay);
        _canMove = true;
    }
}
