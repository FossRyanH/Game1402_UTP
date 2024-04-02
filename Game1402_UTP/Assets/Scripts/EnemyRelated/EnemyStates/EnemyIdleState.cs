using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    readonly int _idleHash = Animator.StringToHash("Idle");
    float _aniamtorDampTime = 0.1f;

    public EnemyIdleState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Animator.CrossFadeInFixedTime(_idleHash, _aniamtorDampTime);
    }

    public override void UpdateState(float delta)
    {
        if (IsInChaseRange())
        {
            _enemy.StateMachine.TransitionTo(new EnemyChaseState(_enemy));
        }

        Move(delta);
    }
}
