using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health;
    public float Health => health;
    public event Action<float> OnHealthchanged;
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        OnHealthchanged?.Invoke(health);
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
