using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySpace
{
    public class InventoryWithSlots : IInventory
    {
        public int Capacity {get;set;}
        public event Action<object, IInventoryItem, int> OnInventoryItemAdded;
        public event Action<object, Type, int> OnInventoryItemRemoved;
        public event Action<object> OnInventoryStateChanged;
        public bool IsFull => _slots.All(_slots=>_slots.IsFull); 
        private List<IInventorySlot> _slots;
        public InventoryWithSlots(int capacity){
            Capacity = capacity;
            _slots = new List<IInventorySlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }

        public bool Add(object sender, IInventoryItem item)
        {
            throw new NotImplementedException();
        }

        public IInventoryItem[] GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            foreach (var slot in _slots)
            {
                if (!slot.IsEmpty){
                    allItems.Add(slot.Item);
                }
            }
            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllItems(Type type)
        {
            var items = new List<IInventoryItem>();
            var slotsOfType = _slots.FindAll(slot =>!slot.IsEmpty && slot.ItemType == type);
            foreach (var slot in slotsOfType){
                items.Add(slot.Item);
            }
            return items.ToArray();
        }

        public IInventoryItem[] GetEquippedItem()
        {
            var items = new List<IInventoryItem>();
            var equippedItems = _slots.FindAll(slot =>!slot.IsEmpty && slot.Item.State.IsEquipped);
            foreach (var slot in equippedItems){
                items.Add(slot.Item);
            }
            return items.ToArray();
        }

        public IInventoryItem GetItem(Type itemType)
        {
            return _slots.Find(slot => slot.ItemType == itemType).Item;
        }

        public int GetItemAmount(Type itemType)
        {
            int amount = 0;
            var allItemSlots = _slots.FindAll(slot =>!slot.IsEmpty && slot.ItemType == itemType);
            foreach (var slot in allItemSlots)
            {
                amount+=slot.Amount;
            }
            return amount;
        }

        public bool HasItem(Type type, out IInventoryItem item)
        {
            item = GetItem(type);
            return item != null;
        }

        public void Remove(object sender, Type itemType, int amount = 1)
        {
            var slotsWithItem = GetAllSlots(itemType);
            if (slotsWithItem.Length == 0){
                return;
            }
            int amountToRemove = amount;
            int count = slotsWithItem.Length;
            for(int i = count -1; i >= 0; i--){
                var slot = slotsWithItem[i];
                if (slot.Amount >= amountToRemove){
                    slot.Item.State.Amount -= amountToRemove;
                    
                    if (slot.Amount <= 0)
                        slot.Clear();

                    OnInventoryStateChanged?.Invoke(sender);
                    OnInventoryItemRemoved?.Invoke(sender,itemType,amountToRemove);
                    break;
                }
                var amountRemoved = slot.Amount;
                amountToRemove -= slot.Amount;
                slot.Clear();
                OnInventoryStateChanged?.Invoke(sender);
                OnInventoryItemRemoved?.Invoke(sender,itemType,amountRemoved);

            }
        }

        public IInventorySlot[] GetAllSlots(Type itemType)
        {
            return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
        }
        public IInventorySlot[] GetAllSlots()
        {
            return _slots.ToArray();
        }
        public bool TryToAdd(object sender, IInventoryItem item)
        {
            var slotsWithSameItemButNoEmpty = _slots.Find(slot=>!slot.IsEmpty && slot.ItemType == item.Type && !slot.IsFull);
            if (slotsWithSameItemButNoEmpty != null){
                return TryToAddToSlot(sender, slotsWithSameItemButNoEmpty, item);
            }
            var emptySlot = _slots.Find(slot => slot.IsEmpty);
            if (emptySlot != null){
                return TryToAddToSlot(sender, emptySlot, item);
            }
            return false;
        }
        public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
        {
            bool fits = slot.Amount + item.State.Amount <= item.Info.MaxItemInInventorySlot;
            int amountToAdd = fits? item.State.Amount : item.Info.MaxItemInInventorySlot - slot.Amount;
            int amointLeft = item.State.Amount - amountToAdd;
            var clonedItem = item.Clone();
            clonedItem.State.Amount = amountToAdd;
            if (slot.IsEmpty)
            {
                slot.SetItem(clonedItem);
            }
            else
            {
                slot.Item.State.Amount += amountToAdd;
            }
            OnInventoryItemAdded?.Invoke(sender,item,amountToAdd);
            OnInventoryStateChanged?.Invoke(sender);
            if (amointLeft <= 0)
                return true;

            item.State.Amount = amointLeft;
            return TryToAdd(sender, item);
        }
        public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
        {
            if (fromSlot.IsEmpty)
                return;

            if (toSlot.IsFull)
                return;

            if (!toSlot.IsEmpty && toSlot.ItemType != fromSlot.ItemType)
                return;

            var slotCapcatity = fromSlot.Capacity;
            var fits = fromSlot.Amount + toSlot.Amount <= slotCapcatity;
            var amountToAdd = fits ? fromSlot.Amount : slotCapcatity - toSlot.Amount;
            var amountLeft = fromSlot.Amount - amountToAdd;

            if (toSlot.IsEmpty)
            {
                toSlot.SetItem(fromSlot.Item);
                fromSlot.Clear();
                OnInventoryStateChanged?.Invoke(sender);
            }

            toSlot.Item.State.Amount += amountToAdd;
            if (fits)
            {
                fromSlot.Clear();
            }
            else
            {
                fromSlot.Item.State.Amount = amountLeft;
            }
            OnInventoryStateChanged?.Invoke(sender);
        }
    }

}
