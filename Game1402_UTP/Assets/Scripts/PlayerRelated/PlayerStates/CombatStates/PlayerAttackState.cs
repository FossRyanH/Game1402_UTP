using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    #region Variables
    AttackData _attack;
    private float _attackTimer;
    #endregion
    
    public PlayerAttackState(PlayerController player)
    {
        this._player = player;
        this._attack = player.Attack;
    }

    public override void EnterState()
    {
        _attackTimer = 0f;
        _player.CanAttack = false;
        _player.Animator.CrossFadeInFixedTime(_attack.AttackName, _attack.AttackTransition);
    }

    public override void UpdateState(float delta)
    {
        _attackTimer += delta;

        if (_attackTimer >= _attack.AttackCooldown)
        {
            _player.CanAttack = true;
            if (_player.Targeter.CurrentTarget == null)
            {
                _player.StateMachine.TransitionTo(_player.StateMachine.LocomotionState);
            }
            else
            {
                _player.StateMachine.TransitionTo(_player.StateMachine.TargetingState);
            }
        }
    }

    // Sets the IsAttacking to false after the attack is complete
    public override void ExitState()
    {
        _player.CanAttack = true;
    }
}
