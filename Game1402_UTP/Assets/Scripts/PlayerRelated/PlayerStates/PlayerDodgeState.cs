using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int _dodgeAnimHash = Animator.StringToHash("Dodge_Backward");
    private float _crossFadeDuration = 0.1f;
    public float DodgeDuration = 0.2f;

    public PlayerDodgeState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.Animator.CrossFadeInFixedTime(_dodgeAnimHash, _crossFadeDuration);
        _player.IsDodging = true;
        _player.CanDodge = false;
        Dodge();
    }

    void Dodge()
    {
        Vector3 newPos = (_player.transform.position + Vector3.back * 1f);

        while (Mathf.Abs((newPos - _player.transform.position).sqrMagnitude) > _player.DodgeLength)
            _player.transform.position = Vector3.Lerp(_player.transform.position, newPos, DodgeDuration * Time.fixedDeltaTime);

        _player.transform.position = newPos;
    }

    public override void UpdateState()
    {
       if (_player.MovementVector == Vector2.zero)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.LocomotionState);
        }
    }

    public override void ExitState()
    {
        //if (_player.IsDodging == false)
        _player.IsDodging = false;
        _player.CanDodge = true;
    }
}
