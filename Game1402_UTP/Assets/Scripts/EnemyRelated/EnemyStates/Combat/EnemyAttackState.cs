using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private string _attackTagName = "Attack";

    public EnemyAttackState(Enemy enemy)
    {
        this._enemy = enemy;
    }
    
    public override void EnterState()
    {
        _enemy.Weapon.SetAttack(_enemy.Attack.Damage, _enemy.Attack.KnockbackDistance);
        _enemy.Animator.CrossFadeInFixedTime(_enemy.Attack.AttackName, _enemy.Attack.AttackTransition);
    }

    public override void UpdateState(float delta)
    {
        if (GetNormalizedTime(_enemy.Animator, _attackTagName) >= 1)
        {
            _enemy.StateMachine.TransitionTo(new EnemyChaseState(_enemy));
        }

        FacePlayer();
    }
}
