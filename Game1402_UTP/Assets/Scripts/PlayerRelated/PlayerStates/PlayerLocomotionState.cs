using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionState : PlayerBaseState
{
    private readonly int _locomotionHash = Animator.StringToHash("Locomotion");
    private readonly int _forwardHash = Animator.StringToHash("YMovement");
    private float _animatorDampTime = 0.1f;
    private float _rotationDamping = 9f;

    private int _attackIndex;
    
    public PlayerLocomotionState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        _player.InputManager.TargetAction += OnTarget;
        _player.Animator.CrossFadeInFixedTime(_locomotionHash, _animatorDampTime);
    }

    public override void UpdateState(float delta)
    {
        if (_player.InputManager.IsAttacking)
        {
            _player.StateMachine.TransitionTo(new PlayerAttackState(_player, _attackIndex));
            return;
        }

        Vector3 movement = HandleMovement();
        Move(movement * _player.MoveSpeed, delta);
        FaceDirection(movement);
        UpdateAnimations();
    }

    public override void ExitState()
    {
        _player.InputManager.TargetAction -= OnTarget;
    }
    
    // Set the input of the players vector2 to a vector 3 plane allowing player to actually move in the world.
    Vector3 HandleMovement()
    {
        Vector3 motion = new Vector3();
        motion.x = _player.MovementVector.x;
        motion.z = _player.MovementVector.y;
        motion.y = 0f;

        motion.Normalize();


        return motion;
    }
    
    // faces the player in the direction of input. Example W faces forward, D to the right... etc
    void FaceDirection(Vector3 inputDir)
    {
        if (inputDir.magnitude > Mathf.Epsilon)
        {
            _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, Quaternion.LookRotation(inputDir), _rotationDamping * Time.fixedDeltaTime);
        }
    }

    void UpdateAnimations()
    {
        if (_player.MovementVector == Vector2.zero)
        {
            _player.MoveSpeed = 0f;
            _player.Animator.SetFloat(_forwardHash, 0f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (Mathf.Abs(_player.MovementVector.y) <= 1f || Mathf.Abs(_player.MovementVector.x) <= 1f)
        {
            _player.MoveSpeed = _player.RunSpeed;
            _player.Animator.SetFloat(_forwardHash, 1f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (Mathf.Abs(_player.MovementVector.y) <= 0.5f || Mathf.Abs(_player.MovementVector.x) <= 0.5f)
        {
            _player.MoveSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_forwardHash, 0.5f, _animatorDampTime, Time.fixedDeltaTime);
        }
    }

    void OnTarget()
    {
        if (!_player.Targeter.HasTarget())
        {
            return;
        }
        _player.StateMachine.TransitionTo(new PlayerTargetingState(_player));
    }
}
