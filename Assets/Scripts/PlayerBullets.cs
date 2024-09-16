using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBullets : MonoBehaviour
{
    [SerializeField] private GameObject _ps;
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    
    [SerializeField] private float _shotSpeed = 40f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _knockback = 25f;
    [SerializeField] private float _size = 1.5f;
    [SerializeField] private float _range = 2;

    private bool _isFading = false;

    private Vector2 _startPos;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(_startPos, transform.position) > _range && !_isFading)
        {
            _isFading = true;
	        StartCoroutine(StaticCoroutines.Fade(0.1f, _sr, Destroy));
        }
    }

    private void Start()
    {
        // Will destroy the bullet after 8 seconds if it doesn't hit anything (Just in case)
	    Destroy(gameObject, 8f);
    }

    public void BulletSetup(Vector3 shootDir, float angle, bool fist)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        // Size
        transform.localScale = new Vector3(_size, _size, _size);

        // Angle/Direction
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Speed
        float vel = _shotSpeed;
        rb.AddForce(shootDir * vel, ForceMode2D.Impulse);
        
        //Sprite
        _sr.sprite = fist ? _spriteList[0] : _spriteList[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // If the bullet hits a wall
        if(collision.tag == "BulletBounds")
        {
            Destroy();
        }
        
        // If the bullet hits an enemy
        else if (collision.GetComponent<EnemyHealth>() != null)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(_rb.velocity.normalized * _knockback, _damage);
            Destroy();
        }
    }

    private void Destroy()
    {
	    // Particles
	    if(_ps != null)
	    {
		    Instantiate(_ps,transform.position,Quaternion.identity);
	    }
        
	    //CameraShaker.Instance.ShakeOnce(2f,2f,0.2f,0.2f);
	    //AudioManager.PlaySound("PoisonBullet");

	    Destroy(gameObject); 
    }
}
