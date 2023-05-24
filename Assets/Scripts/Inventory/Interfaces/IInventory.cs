using System;
namespace InventorySpace
{
    public interface IInventory
    {
        int Capacity {get;set;}
        bool IsFull {get;}

        IInventoryItem GetItem(Type itemType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllItems(Type type);
        IInventoryItem[] GetEquippedItem();
        int GetItemAmount(Type itemType);
        bool TryToAdd(object sender, IInventoryItem item);
        bool Add(object sender, IInventoryItem item);
        void Remove(object sender, Type itemType, int amount = 1);
        bool HasItem(Type type, out IInventoryItem item);
    }
}
