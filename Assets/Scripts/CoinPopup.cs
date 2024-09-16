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
	    _text.color = Color.green;
    }

    private void Start()
    {
        _text.SetText("+" + _text.text + "c");
	    StartCoroutine(Fade(1f));
	    transform.position = new Vector3(transform.position.x + Random.Range(-0.5f,0.5f), transform.position.y + Random.Range(0,0.5f), -0.4f);
    }
    
    
    // Fade out alpha and scale
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
        
	    Destroy();
    }

	private void Destroy()
    {
        Destroy(gameObject);
    }
}