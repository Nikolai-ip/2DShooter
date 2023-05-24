using InventorySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemDroped : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _item;
    [SerializeField] private float _distanceToTake;
    [SerializeField] private float _distanceToDestroy;
    private IInventoryItem _inventoryItem;
    private UIInventory _inventory;
    private Player _player;
    private Transform _tr;
    private bool _isTaked = false;
    [SerializeField] private float _takeSpeed;
    private Rigidbody2D _rb;
    private void Start()
    {
        Type type = Type.GetType(_item.Title);
        _inventoryItem = (IInventoryItem)Activator.CreateInstance(type,(object)_item);
        _inventory = FindObjectOfType<UIInventory>();
        _player = FindObjectOfType<Player>();
        _tr = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();  
    }
    private void Update()
    {
        if (Vector2.Distance(_player.transform.position, _tr.position) < _distanceToTake && !_isTaked)
        {
            _isTaked = true;
            StartCoroutine(MoveToPlayer());
        }
    }
    private IEnumerator MoveToPlayer()
    {
        var delay = new WaitForFixedUpdate();
        while (Vector2.Distance(_player.transform.position, _tr.position) > _distanceToDestroy)
        {
            Vector2 move = (_player.transform.position - _tr.position).normalized * _takeSpeed;
            _rb.velocity += move;
            yield return delay;
        }
        _inventory.Add(_inventoryItem);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distanceToTake);
        Gizmos.DrawWireSphere(transform.position, _distanceToDestroy);
    }
}
