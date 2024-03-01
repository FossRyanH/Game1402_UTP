using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float _attackCooldown = 0.65f;
    private float _attackTimer;
    private readonly int _attackAnimHash = Animator.StringToHash("1HAttack");
    private float _crossFadeDuration = 0.1f;
    
    public PlayerAttackState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.Animator.CrossFadeInFixedTime(_attackAnimHash, _crossFadeDuration);
        _player.IsAttacking = true;
        _attackTimer = 0f;
        _player.CanAttack = false;
    }

    public override void UpdateState(float delta)
    {
        _attackTimer += delta;
        // Once the attack timer reaches the cooldown, sets the players ability to initiate an attack to true
        // Then transitions back to idle.
        if (_attackTimer >= _attackCooldown)
        {
            _player.CanAttack = true;
            _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }
    }

    // Sets the IsAttacking to false after the attack is complete
    public override void ExitState()
    {
        _player.CanAttack = true;
    }
}
