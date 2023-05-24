using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Move")]
    public float speed;

    [Header("Attack state values")]
    [SerializeField] private float _delayBeforeAttack;
    public float DelayBeforeAttack=>_delayBeforeAttack;
    [SerializeField] private float _attackDuration;
    public float AttackDuration => _attackDuration;
    [SerializeField] private float _attackJumpVelocity;
    public float AttackJumpVelocity => _attackJumpVelocity;

    public Collider2D Collider { get; private set; }
    private IdleState _idle;
    public MoveState Move { get; private set; }
    public AttackState Attack { get; private set; }
    public Player Player { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Enemy Instance { get; private set; }
    private State _currentState;
    public bool CanChangeState { get; set; } = true;
    public Transform Transform { get; private set; }
  
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _distanceToMove;
    public float DistanceToMove => _distanceToMove;
    public float DistanceToAttack => _distanceToAttack;


    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Player>();
        Instance = GetComponent<Enemy>();
        Transform = transform;
        _idle = new IdleState(this);
        Move = new MoveState(this);
        Attack = new AttackState(this);
        ChangeState(GetBaseState());
    }

    private void Update()
    {
        try
        {
            TryUpdateLogic();
        }
        catch (Exception e)
        {
            Debug.LogAssertion(e);
        }

    }

    public void ChangeState(State newState)
    {
        if (CanChangeState)
            _currentState = newState;
      //  Debug.Log(_currentState.GetType().Name);
    }
    private void TryUpdateLogic()
    {
        if (_currentState != null)
        {
            _currentState.UpdateLogic();
        }
    }

    private void FixedUpdate()
    {
        try
        {
            TryUpdatePhysic();
        }
        catch (Exception e)
        {
            Debug.LogAssertion(e);
        }
    }

    private void TryUpdatePhysic()
    {
        if (_currentState != null)
        {
            _currentState.UpdatePhysics();
        }
    }

    public State GetBaseState()
    {
        return _idle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceToAttack);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanceToMove);
        Gizmos.color = Color.green;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(Instance.Damage);
        }
    }
}
