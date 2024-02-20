using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
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
        Debug.Log("Leaving Idle State");
    }

}
