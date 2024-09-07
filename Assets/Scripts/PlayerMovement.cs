using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Movement
        _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // Velocity in direction
        _rb.velocity = _movementDirection * _movementSpeed;
    }
}
