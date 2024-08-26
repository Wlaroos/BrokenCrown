using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private BoxCollider2D _bc;
    
    private void Awake()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }
}
