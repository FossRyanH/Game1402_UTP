using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int _combatMovementHash = Animator.StringToHash("CombatMovement");
    private readonly int _yMovement = Animator.StringToHash("YMovement");
    private readonly int _xMovement = Animator.StringToHash("XMovement");
    private float _animatorDampTime = 0.1f;

    public PlayerTargetingState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.InputManager.CancelEvent += OnCancel;
        _player.InputManager.DodgeEvent += HandleDodge;
        _player.Animator.CrossFadeInFixedTime(_combatMovementHash, _animatorDampTime);
        _player.MoveSpeed = _player.WalkSpeed;
    }

    public override void UpdateState(float delta)
    {
        if (_player.Targeter.CurrentTarget == null)
        {
            _player.StateMachine.TransitionTo(new PlayerLocomotionState(_player));
            return;
        }

        if (_player.InputManager.IsAttacking)
        {
            _player.StateMachine.TransitionTo(new PlayerAttackState(_player, 0));
            return;
        }
        if (_player.IsBlocking)
        {
            _player.StateMachine.TransitionTo(new PlayerBlockState(_player));
            return;
        }
        if (_player.Targeter.CurrentTarget == null)
        {
            _player.StateMachine.TransitionTo(new PlayerLocomotionState(_player));
            return;
        }

        Vector3 motion = HandleMovement();
        Move(motion * _player.MoveSpeed, delta);
        FaceTarget();
        UpdateAnimator();
    }

    public override void ExitState()
    {
        _player.InputManager.CancelEvent -= OnCancel;
        _player.InputManager.DodgeEvent -= HandleDodge;
    }

    Vector3 HandleMovement()
    {
        Vector3 motion = new Vector3();
        motion.x = _player.MovementVector.x;
        motion.z = _player.MovementVector.y;
        motion.y = 0f;

        return motion;
    }

    void FaceTarget()
    {
        Vector3 lookPos = _player.Targeter.CurrentTarget.transform.position - _player.transform.position;
        lookPos.y = 0f;

        _player.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    void UpdateAnimator()
    {
        if (_player.MovementVector == Vector2.zero)
        {
            _player.MoveSpeed = 0f;
            _player.Animator.SetFloat(_yMovement, _player.MovementVector.y, _animatorDampTime, Time.fixedDeltaTime);
            _player.Animator.SetFloat(_xMovement, _player.MovementVector.x, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y <= 1f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_yMovement, _player.MovementVector.y, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y <= 0.5f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_yMovement, _player.MovementVector.y, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y < 0f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_yMovement, _player.MovementVector.y, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.x <= 1f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_xMovement, _player.MovementVector.x, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.x < 0f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_xMovement, _player.MovementVector.x, _animatorDampTime, Time.fixedDeltaTime);
        }
    }

    void OnCancel()
    {
        _player.Targeter.CancelTarget();
        _player.StateMachine.TransitionTo(new PlayerLocomotionState(_player));
    }

    void HandleDodge()
    {
        Vector3 motion = HandleMovement();
        if (_player.MovementVector == Vector2.zero) { return; }
        _player.StateMachine.TransitionTo(new PlayerDodgeState(_player, motion));
    }

    void HandleTarget()
    {
        if (!_player.Targeter.HasTarget()) { return; }
        _player.StateMachine.TransitionTo(new PlayerTargetingState(_player));
    }
}
