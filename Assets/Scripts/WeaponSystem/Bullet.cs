using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; } = 0;
    public float Velocity { get; set; } = 0;
    public Vector2 Direction { get; set; }
    private Rigidbody2D _rb;
    [SerializeField] private float _distanceToDisable;
    private float _passedDistance = 0;
    private float _elapsedTime = 0;
    private void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision((int)Mathf.Log(LayerMask.GetMask("Bullet"),2), (int)Mathf.Log(LayerMask.GetMask("Player"),2));
    }
    private void Update()
    {
        _rb.velocity = Direction * Velocity;
        _elapsedTime += Time.deltaTime;
        _passedDistance = _elapsedTime * Velocity;
        if (_passedDistance > _distanceToDisable)
        {
            Disable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(Damage);
            Disable();
        }
    }
    private void Disable()
    {
        Damage = 0;
        Velocity = 0;
        Direction= Vector2.zero;
        _passedDistance = 0;
        _elapsedTime = 0;
        gameObject.SetActive(false);
    }
}
