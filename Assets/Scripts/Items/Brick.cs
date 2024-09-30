using UnityEngine;

public class Brick : BaseItem
{
    [SerializeField] private GameObject _brickPrefab;
    
    protected override void ItemEffects()
    {
        Instantiate(_brickPrefab, new Vector3(Random.Range(-9, 9), Random.Range(-8, 8), 0), Quaternion.identity);
        //var newName = "Brick 2.0";
        //_description = "It's literally just another brick";
        //PlayerStats.Instance.SetItemStats(_name, newName, _description);
    }
    
    protected override void Upgrade()
    {
        ItemEffects();
    }
}
