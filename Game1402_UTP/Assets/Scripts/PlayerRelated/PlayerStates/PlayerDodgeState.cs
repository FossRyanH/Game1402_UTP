using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    float _currentSpeed;

    public PlayerDodgeState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Entering the Dodge State!");
    }

    public override void UpdateState(float delta)
    {
        DirectionHandling();

        if (_player.DodgeTimer == 0f)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }
    }

    Vector3 DirectionHandling()
    {
        Vector3 movementDir = new Vector3();
        movementDir.z = _player.MovementVector.y;
        movementDir.x = _player.MovementVector.x;
        movementDir.y = 0f;

        if (_player.DodgeTimer > 0f)
        {
            movementDir += _player.transform.right * movementDir.x * _player.DodgeLength / _player.DodgeDuration;
            movementDir += _player.transform.forward * movementDir.z * _player.DodgeLength / _player.DodgeDuration;

            _player.DodgeTimer = Mathf.Max(_player.DodgeTimer - Time.deltaTime, 0f);
        }
        else
        {
            movementDir += _player.transform.right * _player.MovementVector.x;
            movementDir += _player.transform.forward * _player.MovementVector.y;
        }

        return movementDir;
    }
}
