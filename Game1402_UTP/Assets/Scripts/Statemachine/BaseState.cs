using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly PlayerController _player;
    protected readonly Animator _animator;

    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    protected const float _crossFadeDuration = 0.15f;

    protected BaseState(PlayerController player, Animator animator)
    {
        this._player = player;
        this._animator = animator;
    }
    
    public virtual void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
