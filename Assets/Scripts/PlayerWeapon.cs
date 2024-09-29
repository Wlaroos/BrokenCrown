using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class PlayerWeapon : MonoBehaviour
{
    public event Action Fired = delegate {};

    [SerializeField] private GameObject _bulletRef;
    
    [SerializeField] private Transform _aimTransform;
    [SerializeField] private Transform _shootTransform1;
    [SerializeField] private Transform _shootTransform2;

    private Vector3 _mousePos;

    [SerializeField] private bool _isAuto;
    [SerializeField] private float _initialFireRate;
    private float _fireRate;
    private float _startFireTime;

    private bool _gunEndPoint = true;
    private bool _allowInput = true;
    
    private PlayerHealth _ph;

    private void Awake()
    {
        _ph = GetComponentInParent<PlayerHealth>();
        _fireRate = _initialFireRate;
    }

    private void OnEnable()
    {
        _allowInput = true;
        _ph.PlayerDeathEvent.AddListener(DisableInput);
        PlayerStats.Instance.StatChangeEvent.AddListener(StatChanges);
    }
    
    private void OnDisable()
    {
        _ph.PlayerDeathEvent.RemoveListener(DisableInput);
        PlayerStats.Instance.StatChangeEvent.RemoveListener(StatChanges);
    }

    private void Update()
    {
        if (_allowInput && Time.timeScale == 1)
        {
            Aim();
            ShootCheck();
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
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        
        // Flip
        /*
        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.x = -1f;
        }
        else
        {
            aimLocalScale.x = 1f;
        }
        transform.localScale = aimLocalScale;
        */
    }

    private void ShootCheck()
    {
        if (_isAuto)
        {
            if (Input.GetMouseButton(0) && Time.time > _startFireTime + (1 / _fireRate))
            {
                //CameraShake.Instance.CamShake();
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _startFireTime + (1 / _fireRate))
            {
                //CameraShake.Instance.CamShake();
                Shoot();
            } 
        }
    }

    private void Shoot()
    {
        //AudioManager.PlaySound("PoisonShot");
        
        // Local variables
        Vector3 aimPosition = _aimTransform.position;
        Vector3 gunEndPointPosition1 = _shootTransform1.position;
        Vector3 gunEndPointPosition2 = _shootTransform2.position;

        Transform bulletTransform;

        Vector3 aimDir = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        
        // Alternating the firing location from two positions
        if (_gunEndPoint)
        {
            // Position 1
             bulletTransform = Instantiate(_bulletRef.transform, gunEndPointPosition1, Quaternion.identity);
        }
        else
        {
            // Position 2
             bulletTransform = Instantiate(_bulletRef.transform, gunEndPointPosition2, Quaternion.identity);
        }
        
        // Aim calculation
        Vector3 shootDir = (aimPosition - transform.position).normalized;
        
        // Create and setup the bullet
        bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, _gunEndPoint);
        
        // Boolean for alternating firing position
        _gunEndPoint = !_gunEndPoint;

        // Recoil
        //transform.GetChild(0).position = transform.GetChild(0).position += (-shootDir * 0.75f);
        //transform.GetChild(0).localRotation = (Quaternion.Euler(0,0,30));

        // Reset starting time for bullet firing calculations
        _startFireTime = Time.time;

        // Event
        Fired?.Invoke();
    }
    
    private void DisableInput()
    {
        _allowInput = false;
    }

    private void StatChanges()
    {
        float fireRate = _initialFireRate * PlayerStats.Instance.FireRateModifier;
        _fireRate = fireRate;
    }
}
