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
        _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var size = _bc.size;
        var orthographicSize = Camera.main.orthographicSize;
        
        transform.position = new Vector3 
        (
            Mathf.Clamp (_rb.position.x, -orthographicSize + size.x / 2, orthographicSize - size.x / 2),
            Mathf.Clamp (_rb.position.y, -orthographicSize + size.y / 2, orthographicSize - size.y / 2),
            0.0f
        );
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementDirection * _movementSpeed;
    }
}
