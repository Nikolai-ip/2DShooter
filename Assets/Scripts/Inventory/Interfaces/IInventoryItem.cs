using System;

namespace InventorySpace{
    public interface IInventoryItem
    {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State {get;}
        Type Type { get; }
        IInventoryItem Clone();
    }
}

