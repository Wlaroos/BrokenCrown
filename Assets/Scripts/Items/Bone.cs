using UnityEngine;

public class Bone : BaseItem
{
    [SerializeField] private GameObject _bonePrefab;
    private bool _firstTime = true;
    private int _boneAmount = 1;

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
            for (int i = 0; i < _boneAmount; i++)
            {
                Instantiate(_bonePrefab, new Vector3(Random.Range(-9, 9), Random.Range(-8, 8), 0), Quaternion.identity); 
            }
        }
    }
    
    protected override void Upgrade()
    {
        _boneAmount++;
    }
    
}
