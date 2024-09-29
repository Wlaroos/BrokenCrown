using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _initialMovementSpeed = 8f;
    private float _movementSpeed;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;
    private PlayerHealth _ph;
    private Vector2 _movementDirection;

    private bool _isKnockback = false;
    
    private Vector3 _mousePos;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _ph = GetComponent<PlayerHealth>();
        
        _movementSpeed = _initialMovementSpeed;
    }

    private void OnEnable()
    {
        _ph.PlayerDeathEvent.AddListener(PlayerDowned);
        PlayerStats.Instance.StatChangeEvent.AddListener(StatChanges);
    }
    
    private void OnDisable()
    {
        _ph.PlayerDeathEvent.RemoveListener(PlayerDowned);
        PlayerStats.Instance.StatChangeEvent.RemoveListener(StatChanges);
    }

    private void Update()
    {
        // Movement
        if (_ph.IsDowned) return;
        _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _movementDirection = Vector2.ClampMagnitude(_movementDirection, 1);

        Aim();
    }

    private void FixedUpdate()
    {
        // Apply velocity in direction
        if (!_isKnockback && !_ph.IsDowned)
        {
            _rb.velocity = _movementDirection * _movementSpeed;
        }

        if (!_ph.IsDowned)
        {
            _anim.SetBool("isMoving", _movementDirection != Vector2.zero);
        }
    }
    
    private void Aim()
    {
        // Mouse position from screen to world point
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;

        // Aiming calculations
        Vector3 aimDir = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        
        // Flip sprite based on where you are aiming
        if (angle > 90 || angle < -90)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
    }

    public void Knockback(Vector2 force, float duration)
    {
        StartCoroutine(KnockbackStart(force, duration));
    }
    
    // Player can't move while knocked back
    // Red is when player can't move, magenta is for iFrames since that code turns the sprite white again
    private IEnumerator KnockbackStart(Vector2 force, float duration)
    {
        _isKnockback = true;
        _rb.AddForce(force * 10, ForceMode2D.Impulse);
        _sr.color = Color.red;
        yield return new WaitForSeconds(duration);
        _sr.color = Color.white;
        _isKnockback = false;
    }
    
    private void PlayerDowned()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _anim.SetBool("isMoving", false);
    }

    private void StatChanges()
    {
        float movementSpeed = _initialMovementSpeed + PlayerStats.Instance.MoveSpeedModifier;
        _movementSpeed = movementSpeed;
    }
}
