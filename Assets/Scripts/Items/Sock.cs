using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : BaseItem
{
    private CircleCollider2D _cc;
    private float _speed = 5f;
    
    protected override void ItemEffects()
    {
       _cc = gameObject.AddComponent<CircleCollider2D>();
       _cc.isTrigger = true;
       _cc.radius = 5f;
       //_description = "More magnets = more reach and speed!";
       //PlayerStats.Instance.SetItemStats(_name, newDescription:_description);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Coin>() != null)
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            other.GetComponent<Rigidbody2D>().velocity = direction * _speed;
        }
    }
    
    protected override void Upgrade()
    {
        _cc.radius += 2f;
        _speed += 4f;
    }
}
