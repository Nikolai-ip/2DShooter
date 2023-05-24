using InventorySpace;
using System;
public class Bag : IInventoryItem
{

    public Type Type => GetType();


    public IInventoryItemInfo Info { get; }

    public IInventoryItemState State { get; }

    public Bag(IInventoryItemInfo info){
        Info = info;
        State = new InventoryItemState();
    }
    public IInventoryItem Clone()
    {
        var clone = new Bag(Info);
        clone.State.Amount= State.Amount;
        return clone;
    }

    }