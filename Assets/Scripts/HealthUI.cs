using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartContainer;
    [SerializeField] private Sprite[] _heartSprites;
    
    private PlayerHealth _playerHealthRef;
    
    private void Awake()
    {
        _playerHealthRef = FindObjectOfType<PlayerHealth>();
        
        for (int i = 0; i < _playerHealthRef.MaxHealth; i++)
        {
            var heart = Instantiate(_heartContainer, transform.position, Quaternion.identity);
            heart.transform.SetParent(transform);
        }
    }
	
    private void OnEnable()
    {
        _playerHealthRef.HealthChangeEvent.AddListener(HealthUpdate);
    }
	
    private void OnDisable()
    {
        _playerHealthRef.HealthChangeEvent.RemoveListener(HealthUpdate);
    }
    
    private void HealthUpdate()
    {
        for (int i = 0; i <  _playerHealthRef.MaxHealth; i++)
        {
            // If the current health is less than the current index, set the sprite to the empty heart sprite, otherwise set it to the full heart sprite
            transform.GetChild(i).GetComponent<Image>().sprite = i <  _playerHealthRef.CurrentHealth ? _heartSprites[0] : _heartSprites[1];
        }
    }
}
