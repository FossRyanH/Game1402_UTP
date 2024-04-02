using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private readonly int _DeathAnimHash = Animator.StringToHash("Death");
    private float _animatorDampTime = 0.1f;
    
    public PlayerDeathState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.Animator.CrossFadeInFixedTime(_DeathAnimHash, _animatorDampTime);
        _player.Health.OnDie += PlayerDeath;
    }

    void PlayerDeath()
    {
        Debug.Log("You've Died");
    }
}
