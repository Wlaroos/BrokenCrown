using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartContainer;
    [SerializeField] private Sprite[] _heartSprites;
    
    private int _maxHealth = 3;
    private int _currentHealth = 3;
    
    private void Awake()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            var heart = Instantiate(_heartContainer, transform.position, Quaternion.identity);
            heart.transform.SetParent(transform);
        }
    }
	
    private void OnEnable()
    {
        PlayerStats.Instance.HealthChangeEvent.AddListener(HealthUpdate);
    }
	
    private void OnDisable()
    {
        PlayerStats.Instance.HealthChangeEvent.RemoveListener(HealthUpdate);
    }
    
    private void HealthUpdate()
    {
        _maxHealth =  PlayerStats.Instance.MaxHealth;
        _currentHealth =  PlayerStats.Instance.CurrentHealth;
        for (int i = 0; i < _maxHealth; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = i < _currentHealth ? _heartSprites[0] : _heartSprites[1];
        }
    }
}
