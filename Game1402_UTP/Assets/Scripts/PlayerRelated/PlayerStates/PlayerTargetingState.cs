using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private float _currentSpeed;
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
        _player.CancelEvent += OnCancel;
        _player.Animator.CrossFadeInFixedTime(_combatMovementHash, _animatorDampTime);
        _currentSpeed = _player.WalkSpeed;
    }

    public override void UpdateState()
    {
        if (_player.Targeter.CurrentTarget == null)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.LocomotionState);
            return;
        }

        Vector3 motion = HandleMovement();
        Move(motion);
        FaceTarget();
        UpdateAnimator();
    }

    public override void ExitState()
    {
        _player.CancelEvent -= OnCancel;
    }

    void Move(Vector3 inputVector)
    {
        _player.Controller.Move((inputVector + _player.Force.Movement) * (_currentSpeed * Time.fixedDeltaTime));
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
            _currentSpeed = 0f;
            _player.Animator.SetFloat(_yMovement, 0f, _animatorDampTime, Time.fixedDeltaTime);
            _player.Animator.SetFloat(_xMovement, 0f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y <= 1f)
        {
            _currentSpeed = _player.RunSpeed;
            _player.Animator.SetFloat(_yMovement, 0.5f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y <= 0.5f)
        {
            _currentSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_yMovement, 0.5f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.y < 0f)
        {
            _currentSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_yMovement, -1f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.x <= 1f)
        {
            _currentSpeed = _player.RunSpeed;
            _player.Animator.SetFloat(_xMovement, 1f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (_player.MovementVector.x < 0f)
        {
            _currentSpeed = _player.RunSpeed;
            _player.Animator.SetFloat(_xMovement, -1f, _animatorDampTime, Time.fixedDeltaTime);
        }
    }

    void OnCancel()
    {
        _player.Targeter.CancelTarget();
        _player.StateMachine.TransitionTo(_player.StateMachine.LocomotionState);
    }
}
