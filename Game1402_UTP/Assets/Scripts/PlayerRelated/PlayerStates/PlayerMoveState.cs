using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    #region Animation Variables
    // sets the String from the animation tree (or animation name it will do both) to an intHash
    //  saves from using string references in multiple places
    readonly int _locomotionHash = Animator.StringToHash("Locomotion");
    // Sets the float name in the animator to the string value listed in the variable.
    readonly int _forwardHash = Animator.StringToHash("YMovement");
    float _animatorDampTime = 0.1f;
    #endregion

    float _currentSpeed;
    // Handles the "smoothness" of rotating in whatever direction is being fed into the movement input.
    float _rotationDamping = 12f;

    public PlayerMoveState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Move State Entered");
        // Sets the animator to update animations over a span of time to make a smooth transition of animations.
        _player.Animator.CrossFadeInFixedTime(_locomotionHash, _animatorDampTime);
    }

    public override void UpdateState(float delta)
    {
        Vector3 movement = HandleMovement();
        Move(movement);
        FaceDirection(movement);
        UpdateAnimations();
        //  If input is nothing, transition the player back to the idle state
        if (movement == Vector3.zero)
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }
    }

    // Set the input of the players vector2 to a vector 3 plane allowing player to actually move in the world.
    Vector3 HandleMovement()
    {
        Vector3 motion = new Vector3();
        motion.x = _player.MovementVector.x;
        motion.z = _player.MovementVector.y;
        motion.y = 0f;

        return motion;
    }

    // Processes the player's movement and multiplies it by the value depending on the value of the input
    void Move(Vector3 inputVector)
    {
        _player.Controller.Move((inputVector + _player.Force.Movement) * _currentSpeed * Time.fixedDeltaTime);
    }

    // faces the player in the direction of input. Exmaple W faces forward, D to teh right... etc
    void FaceDirection(Vector3 inputDir)
    {
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, Quaternion.LookRotation(inputDir), _rotationDamping * Time.deltaTime);
    }

    // Updates animator depedning on player input, also sets the movement speed.
    void UpdateAnimations()
    {
        if (_player.MovementVector == Vector2.zero)
        {
            _currentSpeed = 0f;
            _player.Animator.SetFloat(_forwardHash, 0f, _animatorDampTime, Time.deltaTime);
        }
        else if (Mathf.Abs(_player.MovementVector.y) <= 1f || Mathf.Abs(_player.MovementVector.x) <= 1f)
        {
            _currentSpeed = _player.RunSpeed;
            _player.Animator.SetFloat(_forwardHash, 1f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else if (Mathf.Abs(_player.MovementVector.y) <= 0.5f || Mathf.Abs(_player.MovementVector.x) <= 0.5f)
        {
            _currentSpeed = _player.WalkSpeed;
            _player.Animator.SetFloat(_forwardHash, 0.5f, _animatorDampTime, Time.fixedDeltaTime);
        }
        else
        {
            _currentSpeed = 0f;
        }
    }
}
