using InventorySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryTester
{
    private InventoryItemInfo _helm;
    private InventoryItemInfo _cloack;
    private InventoryItemInfo _bag;
    private UIInventorySlot[] _uiSlots;
    public InventoryWithSlots Inventory { get; }
    public UIInventoryTester(InventoryItemInfo helm, InventoryItemInfo cloack, InventoryItemInfo bag, UIInventorySlot[] uiSlots)
    {
        _helm = helm;
        _cloack = cloack;
        _bag = bag;
        _uiSlots = uiSlots;
        
        Inventory.OnInventoryStateChanged += OnInventoryStateChanged;
    }

    public void FillSlots()
    {
        var allSlots = Inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);
        var filledSlots = 5;
        for (int i = 0; i < filledSlots/2; i++)
        {
            var filledSlot = AddRandomBagsIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomHelmsIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }
        SetupInventoryUI(Inventory);
    }
    private IInventorySlot AddRandomBagsIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 5);
        var bag = new Bag(_bag);
        bag.State.Amount = rCount;
        Inventory.TryToAddToSlot(this, rSlot, bag);
        return rSlot;
    }
    private IInventorySlot AddRandomHelmsIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 5);
        var helm = new Helm(_helm);
        helm.State.Amount = rCount;
        Inventory.TryToAddToSlot(this, rSlot, helm);
        return rSlot;
    }
    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();

        }
    }
    private void OnInventoryStateChanged(object sender)
    {
        foreach (var slot in _uiSlots)
        {
            slot.Refresh();
        }
    }

}
