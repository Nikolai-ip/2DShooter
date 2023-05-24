using System;
using System.Collections;
using System.Collections.Generic;
using InventorySpace;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private IInventory _inventory;
    private void Awake() {
        _inventory = new InventoryWithSlots(10);
        Debug.Log("Inventory was create, capacity 10");
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)){
            AddRandomBags();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            RemoveRandomBags();
        }
    }

    private void RemoveRandomBags()
    {
        int count = UnityEngine.Random.Range(1,5);
        _inventory.Remove(this,typeof(Bag),count);
    }

    private void AddRandomBags()
    {
        int count = UnityEngine.Random.Range(1,5);
    }
}
