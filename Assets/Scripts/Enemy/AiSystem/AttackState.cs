using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackState : State
{
    private bool _canAttack = true;
    public override void UpdateLogic()
    {
        if (GetDistanceToPlayer() > stateMachine.DistanceToAttack)
        {
            stateMachine.ChangeState(stateMachine.Move);
        }
    }

    public override  void UpdatePhysics()
    {
        if (_canAttack)
        {
             Attack();
        }
    }
    private async void Attack()
    {

        _canAttack = false;
        stateMachine.CanChangeState = false;
        Vector2 dir = GetDirToPlayer();
        await Task.Delay((int)(stateMachine.DelayBeforeAttack*1000));
        if (rb == null) return;
        JumpAttack(dir);
        await Task.Delay((int)(stateMachine.AttackDuration * 1000));
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        _canAttack = true;
        stateMachine.CanChangeState = true;
    }
    private void JumpAttack(Vector2 dir)
    {
        rb.AddForce(dir * stateMachine.AttackJumpVelocity, ForceMode2D.Impulse);
    }
    public AttackState(StateMachine stateMachine) : base(stateMachine)
    {
    }
    
}
