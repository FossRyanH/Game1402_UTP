using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    readonly int _idleHash = Animator.StringToHash("Locomotion");
    float _crossFadeDuration = 0.1f;

    public PlayerIdleState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.Animator.CrossFadeInFixedTime(_idleHash, _crossFadeDuration);
        _player.Animator.SetFloat("YMovement", 0f);
    }

    public override void UpdateState(float delta)
    {
        if (Mathf.Abs(_player.MovementVector.x) > 0.1f || Mathf.Abs(_player.MovementVector.y) > 0.1f)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);
        }
    }

    public override void ExitState()
    {
    }
}
