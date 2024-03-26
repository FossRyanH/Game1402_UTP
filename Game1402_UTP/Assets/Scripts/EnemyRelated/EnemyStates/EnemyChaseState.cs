using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    readonly int _enemyChaseHash = Animator.StringToHash("RunFWD");
    float _animatorDampTime = 0.1f;

    public EnemyChaseState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Animator.CrossFadeInFixedTime(_enemyChaseHash, _animatorDampTime);
    }

    public override void UpdateState()
    {
        if (!IsInChaseRange())
        {
            _enemy.StateMachine.TransitionTo(_enemy.StateMachine.EnemyIdleState);
        }
    }
}
