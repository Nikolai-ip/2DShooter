using InventorySpace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryWithSlots Inventory { get; private set; }
    private bool _inventoryIsVisble = false;
    [SerializeField] private Animator _gridAmimator;
    private UIInventorySlot[] _uiSlots;
    private void Start()
    {
        Inventory = new InventoryWithSlots(15);
    }
    public void Add(IInventoryItem item)
    {
        _uiSlots = GetComponentsInChildren<UIInventorySlot>();
        var slots = Inventory.GetAllSlots();
        var freeSlot = slots.Where(slot => slot.IsEmpty || (slot.ItemType == item.GetType())).FirstOrDefault();
        Inventory.TryToAddToSlot(this, freeSlot, item);
        SetupInventoryUI();
    }
    private void SetupInventoryUI()
    {
        var allSlots = Inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();

        }
    }
    public void ShowOrHideInventory()
    {
        if (!_inventoryIsVisble)
        {
            _inventoryIsVisble = true;
            _gridAmimator.SetTrigger("Appearence");
        }
        else
        {
            _inventoryIsVisble = false;
            _gridAmimator.SetTrigger("Disappearence");

        }
    }
}
