using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

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
            _enemy.StateMachine.TransitionTo(new EnemyIdleState(_enemy));
            return;
        }
        else if (IsInAttackRange())
        {
            _enemy.StateMachine.TransitionTo(new EnemyAttackState(_enemy));
        }

        MoveToPlayer(delta);

        FacePlayer();
    }

    public override void ExitState()
    {
        _enemy.Agent.ResetPath();
        _enemy.Agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float delta)
    {
        if (_enemy.Agent.isOnNavMesh)
        {
            _enemy.Agent.destination = _enemy.Player.transform.position;
            Move(_enemy.Agent.desiredVelocity.normalized * _enemy.MoveSpeed, delta);
        }

        _enemy.Agent.velocity = _enemy.Controller.velocity;
    }

    bool IsInAttackRange()
    {
        if (_enemy.Player.IsDead) { return false; }

        float playerDistanceSqr = (_enemy.Player.transform.position - _enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _enemy.AttackRange * _enemy.AttackRange;
    }
}
