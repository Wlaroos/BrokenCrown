using System.Collections;
using UnityEngine;

public class FadeDelete : MonoBehaviour
{
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Color initialColor = _sr.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        
        float elapsedTime = 0f;
        
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            _sr.color = Color.Lerp(initialColor, targetColor, elapsedTime / 2f);
            yield return null;
        }
        
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
