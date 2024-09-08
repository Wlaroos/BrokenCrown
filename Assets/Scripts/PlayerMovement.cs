using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc;
    private Vector2 _movementDirection;
    
    private Vector3 _mousePos;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Movement
        _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Aim();
    }

    private void FixedUpdate()
    {
        // Velocity in direction
        _rb.velocity = _movementDirection * _movementSpeed;
    }
    
    private void Aim()
    {
        // Mouse position from screen to world point
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;

        // Aiming calculations
        Vector3 aimDir = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        
        // Flip
        if (angle > 90 || angle < -90)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
    }
}
