using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void StartFade()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1.2f));
    }

    // Fade out alpha and scale
    private IEnumerator Fade(float fadeDuration)
    {
        _text.alpha = 1;
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            
            _text.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

            yield return null;
        }
    }
}
