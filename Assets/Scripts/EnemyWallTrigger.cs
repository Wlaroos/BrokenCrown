using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyHealth>() != null)
        {
            other.gameObject.layer = LayerMask.NameToLayer("Enemy");
            other.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }
}
