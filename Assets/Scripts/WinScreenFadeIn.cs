using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenFadeIn : MonoBehaviour
{
    private SpriteRenderer _sr;
    private TMP_Text _text;
    private PlayerHealth _playerHealth;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _text = GetComponentInChildren<TMP_Text>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnEnable()
    {
        WaveSpawner.Instance.FinalWaveCompleteEvent.AddListener(StartFadeIn);
    }

    private void OnDisable()
    {
        WaveSpawner.Instance.FinalWaveCompleteEvent.RemoveListener(StartFadeIn);
    }
    
    private void StartFadeIn()
    {
        StartCoroutine(FadeIn(2f));
    }

    private IEnumerator FadeIn(float fadeDuration)
    {
        _sr.color = Color.clear;
        _text.alpha = 0;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            
            _sr.color = Color.Lerp(Color.clear, new Color32(0,0,0,150), elapsedTime / fadeDuration);
            _text.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            
            yield return null;
        }
    }
}
