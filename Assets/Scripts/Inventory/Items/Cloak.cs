using InventorySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : IInventoryItem
{
    public Type Type => GetType();

    public IInventoryItemInfo Info { get; }

    public IInventoryItemState State { get; }

    public Cloak(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    public IInventoryItem Clone()
    {
        var clone = new Cloak(Info);
        clone.State.Amount = State.Amount;
        return clone;
    }
}
