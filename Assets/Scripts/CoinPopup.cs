using System.Collections;
using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _text.SetText(_text.text);

        // Change color of text based on value
        switch (_text.text)
        {
            case "0.01":
            {
                _text.color = new Color32(184, 115, 51, 255);
                break;
            }
            case "0.25":
            {
                _text.color = new Color32(210, 175, 0, 255);
                break;
            }
            default:
            {
                _text.color = new Color32(210, 210, 210, 255);
                break;
            }
        }
        
        // Fade out
	    StartCoroutine(Fade(.67f));
        
        // Move above player
	    transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, -0.4f);
    }
    
    
    // Fade out alpha and scale
    private IEnumerator Fade(float fadeDuration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            
            elapsedTime += Time.deltaTime;
            
            //var scale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / fadeDuration);
            _text.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

            //transform.localScale = scale;
            yield return null;
        }
        
	    Destroy();
    }

	private void Destroy()
    {
        Destroy(gameObject);
    }
}