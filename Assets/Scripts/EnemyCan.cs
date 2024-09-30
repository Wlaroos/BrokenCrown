using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(transform.right,1);
            Destroy(gameObject);
        }
        else if (other.GetComponent<PlayerBullets>() != null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
