using InventorySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : IInventoryItem
{
    public Type Type => GetType();

    public IInventoryItemInfo Info { get; }

    public IInventoryItemState State { get; }

    public Helm(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    public IInventoryItem Clone()
    {
        var clone = new Helm(Info);
        clone.State.Amount = State.Amount;
        return clone;
    }
}
