using UnityEngine;

public class Brick : BaseItem
{
    [SerializeField] private GameObject _brickPrefab;
    
    protected override void ItemEffects()
    {
        Instantiate(_brickPrefab, new Vector3(Random.Range(-9, 9), Random.Range(-8, 8), 0), Quaternion.identity);
    }
    
    protected override void Upgrade()
    {
        ItemEffects();
    }
}
