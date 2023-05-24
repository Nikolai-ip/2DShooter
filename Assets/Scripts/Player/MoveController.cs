using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speedMovement;
    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();   
    }
    public void Move(Vector2 move)
    {
        _rb.velocity = move *_speedMovement;
    }
}
