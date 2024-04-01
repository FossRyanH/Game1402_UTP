using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float _previousFrameTime;
    private bool _alreadyAppliedForce;

    private AttackData _attack;
    private string _attackTagString = "Attack";
    
    public PlayerAttackState(PlayerController player, int attackIndex)
    {
        this._player = player;
        _attack = _player.Attack[attackIndex];
    }

    public override void EnterState()
    {
        _player.Weapon.SetAttack(_attack.Damage, _attack.KnockbackDistance);
        _player.Animator.CrossFadeInFixedTime(_attack.AttackName, _attack.AttackTransition);
    }

    public override void UpdateState(float delta)
    {
        float normalizedTime = GetNormalizedTime(_player.Animator, _attackTagString);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= _attack.ForceTime)
            {
                TryApplyForce();
            }

            if (_player.InputManager.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            ReturnToLocomotion();
        }
        _previousFrameTime = normalizedTime;
    }

    public override void ExitState()
    {
        _player.InputManager.IsAttacking = false;
    }

    void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) { return; }

        if (_attack.ComboStateIndex % _player.Attack.Length == 0) { return; }
        
        if (normalizedTime < _attack.ComboAttackTime) { return; }

        _player.StateMachine.TransitionTo(new PlayerAttackState(_player, _attack.ComboStateIndex));
    }

    void TryApplyForce()
    {
        if (_alreadyAppliedForce) { return; }

        _player.Force.AddForce(_player.transform.forward * _attack.Force);

        _alreadyAppliedForce = true;
    }

}
