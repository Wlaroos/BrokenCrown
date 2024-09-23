using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName = "ItemSO", menuName = "NewItem")]

public class ItemSO : ScriptableObject
{
    [SerializeField] private string _itemName;
    
    [SerializeField] private Sprite _itemImage;
    
    [TextArea(3, 10)]
    [SerializeField] private string _itemDescription;
    
    [SerializeField] private float _itemCost;
    
    [SerializeField] private BaseItem _item;
    
    public string ItemName => _itemName;
    public Sprite ItemImage => _itemImage;
    public string ItemDescription => _itemDescription;
    public float ItemCost => _itemCost;
    public BaseItem Item => _item;
    
#if UNITY_EDITOR
    [ContextMenu("Rename to name")]
    private void Rename()
    {
        string assetPath =  AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, _itemName);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Delete this")]
    private void DeleteThis()
    {
        Undo.DestroyObjectImmediate(this);
        AssetDatabase.SaveAssets();
    }
#endif


}