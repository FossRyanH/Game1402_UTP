using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Attacking yo.");
    }

    public override void UpdateState(float delta)
    {
        if (!_player.IsAttacking)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Attack State");
    }
}
