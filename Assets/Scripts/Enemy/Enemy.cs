using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float _damage;
    public float Damage { get { return _damage;} }
    protected override void Die()
    {
        base.Die();
        GetComponent<EnemyDropController>().DropLoot();
    }
}
