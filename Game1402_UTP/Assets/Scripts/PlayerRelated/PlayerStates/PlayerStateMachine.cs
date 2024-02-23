using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStateMachine : StateMachine
{
    #region State
    public PlayerIdleState IdleState;
    public PlayerMoveState MoveState;
    public PlayerAttackState AttackState;
    public PlayerDodgeState DodgeState;
    #endregion
    
    // Gets a reference to all the states available to the player.
    public PlayerStateMachine(PlayerController player)
    {
        this.IdleState = new PlayerIdleState(player);
        this.MoveState = new PlayerMoveState(player);
        this.AttackState = new PlayerAttackState(player);
        this.DodgeState = new PlayerDodgeState(player);
    }
}
