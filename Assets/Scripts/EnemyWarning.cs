using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StaticCoroutines.Fade(2f, GetComponent<SpriteRenderer>(), Destroy));   
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
