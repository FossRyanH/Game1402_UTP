using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    PlayerController _player;

    readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    float _crossFadeDuration = 0.1f;

    public PlayerIdleState(PlayerController player)
    {
        this._player = player;
    }

    public void EnterState()
    {
        Debug.Log("Entering Idle State");
        _player.Animator.CrossFadeInFixedTime(LocomotionHash, _crossFadeDuration);
    }

}
