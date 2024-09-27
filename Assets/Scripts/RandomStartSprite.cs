using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private SpriteRenderer _sr;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _sprites[Random.Range(0, _sprites.Length)];
        _sr.flipX = Random.Range(0, 2) == 0;
    }
}
