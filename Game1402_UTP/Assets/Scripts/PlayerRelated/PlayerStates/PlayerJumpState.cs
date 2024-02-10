using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    PlayerController _player;

    readonly int _jumpHash = Animator.StringToHash("");

    public PlayerJumpState(PlayerController player)
    {
        this._player = player;
    }

    public void EnterState()
    {
        Debug.Log("Jumping");
        Jump();
    }

    void Jump()
    {
        Vector3 jumpVelocity = _player.Rb.velocity;
        jumpVelocity.y = _player.JumpForce;
        _player.Rb.velocity = jumpVelocity;
    }
}
