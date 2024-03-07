using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int _dodgeAnimHash = Animator.StringToHash("Dodge_Backward");
    private float _crossFadeDuration = 0.1f;
    public float DodgeDuration = 1.25f;
    //private float t = 0.3f;

    public PlayerDodgeState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.Animator.CrossFadeInFixedTime(_dodgeAnimHash, _crossFadeDuration);
        _player.IsDodging = true;
        //_player.CanDodge = true;
    }

    public override void UpdateState(float delta)
    {
        Vector3 newPos = _player.transform.position + (50 * Vector3.back);

        while (Mathf.Abs((newPos - _player.transform.position).sqrMagnitude) > 0.05f)
            _player.transform.position = Vector3.Lerp(_player.transform.position, newPos, DodgeDuration);

        _player.transform.position = newPos;
    }

    public override void ExitState()
    {
    }
}
