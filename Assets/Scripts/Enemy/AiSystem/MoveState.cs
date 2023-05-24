using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public override void UpdateLogic()
    {
        if (GetDistanceToPlayer() < stateMachine.DistanceToAttack)
        {
            rb.velocity = Vector2.zero;

            stateMachine.ChangeState(stateMachine.Attack);
        }
        if (GetDistanceToPlayer() > stateMachine.DistanceToMove)
        {
            stateMachine.ChangeState(stateMachine.GetBaseState());
        }
    }

    public override void UpdatePhysics()
    {
        FollowForPlayer();
    }

    private void FollowForPlayer()
    {
        rb.velocity = GetDirToPlayer() * stateMachine.speed;
    }

    public MoveState(StateMachine stateMachine) : base(stateMachine)
    {
    }
}
