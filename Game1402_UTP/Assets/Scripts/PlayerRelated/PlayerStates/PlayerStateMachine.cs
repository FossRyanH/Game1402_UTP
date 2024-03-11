using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStateMachine : StateMachine
{
    #region State
    public PlayerLocomotionState LocomotionState;
    public PlayerAttackState AttackState;
    public PlayerDodgeState DodgeState;
    public PlayerTargetingState TargetingState;
    public PlayerDeathState DeathState;
    #endregion
    
    // Gets a reference to all the states available to the player.
    public PlayerStateMachine(PlayerController player)
    {
        this.LocomotionState = new PlayerLocomotionState(player);
        this.AttackState = new PlayerAttackState(player);
        this.TargetingState = new PlayerTargetingState(player);
        this.DodgeState = new PlayerDodgeState(player);
        this.DeathState = new PlayerDeathState(player);
    }
}
