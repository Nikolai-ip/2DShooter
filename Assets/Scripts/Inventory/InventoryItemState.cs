using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    [SerializeField] private int _amount;
    [SerializeField] private bool _isEquipped;
    public int Amount { get => _amount; set => _amount = value; }
    public bool IsEquipped { get => _isEquipped; set => _isEquipped = value; }
    public InventoryItemState() 
    {
        _amount = 0;
        _isEquipped = false;
    }

}
