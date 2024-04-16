using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int _dodgeAnimHash = Animator.StringToHash("Dodge");
    private float _crossFadeDuration = 0.1f;

    private float _remainingDodgeTime;
    private Vector3 _dodgingDirectionInput;

    public PlayerDodgeState(PlayerController player, Vector3 dodgeDirInput)
    {
        this._player = player;
        this._dodgingDirectionInput = dodgeDirInput;
    }

    public override void EnterState()
    {
        _remainingDodgeTime = _player.DodgeDuration;
        _player.MoveSpeed = _player.DodgeSpeed;

        _player.Animator.CrossFadeInFixedTime(_dodgeAnimHash, _crossFadeDuration);
    }

    public override void UpdateState(float delta)
    {
        float dodgeAmount = _player.DodgeLength / _player.DodgeDuration;

        Move(_dodgingDirectionInput * _player.MoveSpeed * dodgeAmount, delta);

        _remainingDodgeTime -= Time.deltaTime;

        if (_remainingDodgeTime <= 0f)
        {
            _player.StateMachine.TransitionTo(new PlayerTargetingState(_player));
        }
    }
}
