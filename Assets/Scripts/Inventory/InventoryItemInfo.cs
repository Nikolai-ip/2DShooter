using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create new itemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _title;
    [SerializeField] private int _maxItemInInventorySlot;

    public string Id => _id;

    public string Title => _title;

    public string Description => _description;


    public Sprite Icon => _icon;

    public int MaxItemInInventorySlot => _maxItemInInventorySlot;
}
