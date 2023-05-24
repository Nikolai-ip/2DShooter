using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void UpdateLogic()
    {
        if (GetDistanceToPlayer() < stateMachine.DistanceToMove)
        {
            stateMachine.ChangeState(stateMachine.Move);
        }
    }

    public override void UpdatePhysics()
    {
        rb.velocity = Vector2.zero;
    }

    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }
}
