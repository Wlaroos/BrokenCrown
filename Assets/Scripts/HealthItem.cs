using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private GameObject _healthPopup;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite[] _eatenSprite;

    [SerializeField] private BoxCollider2D _bc1;
    [SerializeField] private BoxCollider2D _bc2;
    
    private SpriteRenderer _sr;
    private readonly int _healAmount = 1;
    private int _rand;
    
    private void Awake()
    {
        _rand = Random.Range(0, _sprites.Length);
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _sprites[_rand];
        
        _bc1.size = _sprites[_rand].bounds.size;
        _bc2.size = _sprites[_rand].bounds.size;
    }

    private void OnEnable()
    {
        PlayerStats.Instance.ShopScreenChangeEvent.AddListener(OnDestroy);
    }
    
    private void OnDisable()
    {
        PlayerStats.Instance.ShopScreenChangeEvent.RemoveListener(OnDestroy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            if (other.GetComponent<PlayerHealth>().CurrentHealth < other.GetComponent<PlayerHealth>().MaxHealth)
            {
                other.GetComponent<PlayerHealth>().Heal(_healAmount);
                Used();
            }
        }
    }

    private void Used()
    {
        _sr.sprite = _eatenSprite[_rand];
        
        Instantiate(_healthPopup, transform.position + Vector3.up * 2, Quaternion.identity);
        
        GetComponent<BoxCollider2D>().enabled = false;
        
        StartCoroutine(StaticCoroutines.Fade(2f, _sr));
        
        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
