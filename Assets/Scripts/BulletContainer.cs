using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private int _initialBulletCapacity;
    private PoolMono<Bullet> _pool;
    public PoolMono<Bullet> Pool => _pool;
    private void Start()
    {
        _pool = new PoolMono<Bullet>(_bullet, _initialBulletCapacity, transform);
        _pool.AutoExpand = true; 
    }

}
