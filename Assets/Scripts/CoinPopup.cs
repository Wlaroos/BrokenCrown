using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.color = Color.white;
    }

    private void Start()
    {
        _text.SetText("+" + _text.text + "c");
        StartCoroutine(Fade(1f));
    }
    
    private IEnumerator Fade(float fadeDuration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            
            elapsedTime += Time.deltaTime;
            
            var scale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / fadeDuration);
            _text.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

            transform.localScale = scale;
            yield return null;
        }
        
        Remove();
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}