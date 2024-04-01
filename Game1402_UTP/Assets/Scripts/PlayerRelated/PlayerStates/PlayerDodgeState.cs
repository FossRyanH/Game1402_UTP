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
        Vector3 movement = new Vector3();
        movement += _player.transform.right * _dodgingDirectionInput.x * _player.DodgeLength / _player.DodgeDuration;
        movement += _player.transform.forward * _dodgingDirectionInput.y * _player.DodgeLength / _player.DodgeDuration;

        Move(movement, delta);

        _remainingDodgeTime -= Time.deltaTime;

        if (_remainingDodgeTime <= 0f)
        {
            _player.StateMachine.TransitionTo(new PlayerTargetingState(_player));
        }
    }
}
