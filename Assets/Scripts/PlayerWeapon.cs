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
    [SerializeField] private float _fireDelay;
    private float _startFireTime;

    private bool _gunEndPoint = true;
    private bool _allowInput = true;

    [SerializeField] private float _bulletSize;

    private void OnEnable()
    {
        _allowInput = true;
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
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;

        Vector3 aimDir = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90);

        /*
         Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.x = 1f;
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
            if (Input.GetMouseButton(0) && Time.time > _fireDelay + _startFireTime)
            {
                //CameraShake.Instance.CamShake();
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _startFireTime + _fireDelay)
            {
                //CameraShake.Instance.CamShake();
                Shoot();
            } 
        }
    }

    private void Shoot()
    {
        //AudioManager.PlaySound("PoisonShot");
        
        Vector3 aimPosition = _aimTransform.position;
        Vector3 gunEndPointPosition1 = _shootTransform1.position;
        Vector3 gunEndPointPosition2 = _shootTransform2.position;

        Vector3 aimDir = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        
        if (_gunEndPoint)
        {
            Transform bulletTransform = Instantiate(_bulletRef.transform, gunEndPointPosition1, Quaternion.identity);
            Vector3 shootDir = (aimPosition - transform.position).normalized;
            bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, 20, 1, 3, _bulletSize);
        }
        else
        {
            Transform bulletTransform = Instantiate(_bulletRef.transform, gunEndPointPosition2, Quaternion.identity);
            Vector3 shootDir = (aimPosition - transform.position).normalized;
            bulletTransform.GetComponent<PlayerBullets>().BulletSetup(shootDir, angle, 20, 1, 3, _bulletSize);
        }
        
        _gunEndPoint = !_gunEndPoint;

        // Recoil
        //transform.GetChild(0).position = transform.GetChild(0).position += (-shootDir * 0.75f);
        //transform.GetChild(0).localRotation = (Quaternion.Euler(0,0,30));

        _startFireTime = Time.time;

        // Event
        Fired?.Invoke();
    }
}
