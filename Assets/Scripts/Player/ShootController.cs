using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    public Weapon CurrentWeapon => _currentWeapon;  
    [SerializeField] private LayerMask _targetLayer;
    private Vector2 _targetPos;
    [Range(0f,360f)]
    [SerializeField] private float _rotateAngle;
    public event Action<Enemy> OnTargeting;
    private bool _fireMode = false;
    private void Start()
    {
        UpdateWeapon();
    }
    public void UpdateWeapon()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        FindTarget();
        _currentWeapon.RotateWeapon(_targetPos,_rotateAngle);
        if (_fireMode)
        {
            _currentWeapon?.Shoot(_targetPos);
        }
    }

    public void EnableFireMode()
    {
        _fireMode = true;
    }
    public void DisableFireMode()
    {
        _fireMode = false;
    }
    private void FindTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _currentWeapon.GetDistance, _targetLayer);
        Collider2D target = targets.OrderBy(coll => Vector2.Distance(transform.position, coll.transform.position)).FirstOrDefault();
        if (target != null)
        {
            _targetPos = target.transform.position;
            if (target.TryGetComponent(out Enemy enemy))
            {
                OnTargeting?.Invoke(enemy);
            }
        }
        else
        {
            OnTargeting?.Invoke(null);
            _targetPos = Vector2.zero;

        }
    }
    private void OnDrawGizmos()
    {
        if (_currentWeapon != null)
        {
            Gizmos.DrawWireSphere(transform.position, _currentWeapon.GetDistance);
        }
    }

}
