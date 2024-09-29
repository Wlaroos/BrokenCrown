using UnityEngine;

public class Bone : BaseItem
{
    [SerializeField] private GameObject _bonePrefab;
    private bool _firstTime = true;

    private void OnEnable()
    {
        PlayerStats.Instance.FightScreenChangeEvent.AddListener(ItemEffects);
    }
    
    private void OnDisable()
    {
        PlayerStats.Instance.FightScreenChangeEvent.RemoveListener(ItemEffects);
    }
    
    protected override void ItemEffects()
    {
        if (_firstTime)
        {
            _firstTime = false;
        }
        else
        {
            Instantiate(_bonePrefab, new Vector3(Random.Range(-9, 9), Random.Range(-8, 8), 0), Quaternion.identity);
        }
    }
}
