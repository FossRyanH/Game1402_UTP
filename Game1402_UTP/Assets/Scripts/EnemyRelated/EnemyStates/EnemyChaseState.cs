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
        _enemy.MoveSpeed = 0.95f;
        _enemy.Animator.CrossFadeInFixedTime(_enemyChaseHash, _animatorDampTime);
    }

    public override void UpdateState(float delta)
    {
        if (!IsInChaseRange())
        {
            _enemy.StateMachine.TransitionTo(_enemy.StateMachine.EnemyIdleState);
            return;
        }

        MoveToPlayer(delta);
    }

    public override void ExitState()
    {
        _enemy.Agent.ResetPath();
        _enemy.Agent.velocity = Vector3.zero;
    }

    void Move(Vector3 motion, float delta)
    {
        _enemy.Controller.Move((motion + _enemy.ForceReciever.Movement) * delta);
    }

    void MoveToPlayer(float delta)
    {
        if (_enemy.Agent.isOnNavMesh)
        {
            _enemy.Agent.destination = _enemy.Player.transform.position;
            Move(_enemy.Agent.desiredVelocity.normalized, delta);
        }

        _enemy.Agent.velocity = _enemy.Controller.velocity;
    }
}
