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
    #endregion
    
    public PlayerStateMachine(PlayerController player)
    {
        this.IdleState = new PlayerIdleState(player);
        this.MoveState = new PlayerMoveState(player);
    }
}
