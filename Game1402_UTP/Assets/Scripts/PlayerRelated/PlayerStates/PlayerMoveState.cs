using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    float _currentSpeed;
    // Handles the "smoothness" of rotating in whatever direction is being fed into the movement input.
    float _rotationDamping = 10f;

    public PlayerMoveState(PlayerController player)
    {
        this._player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Move State Entered");
    }

    public override void UpdateState(float delta)
    {
        Vector3 movement = HandleMovement();
        Move(movement);
        FaceDirection(movement);
        // If there's no movement being input the player returns to the Idle state.
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
        _player.Controller.Move((inputVector + _player.Force.Movement) * MarkSpeed(_currentSpeed) * Time.deltaTime);
    }

    // faces the player in the direction of input. Exmaple W faces forward, D to teh right... etc
    void FaceDirection(Vector3 inputDir)
    {
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, Quaternion.LookRotation(inputDir), _rotationDamping * Time.deltaTime);
    }

    // Depending on the value of input changes player speed accordingly
    float MarkSpeed(float speed)
    {
        if (Mathf.Abs(_player.MovementVector.y) <= 1f || Mathf.Abs(_player.MovementVector.x) <= 1f)
        {
            speed = _player.RunSpeed;
        }
        else if (Mathf.Abs(_player.MovementVector.y) <= 0.5f || Mathf.Abs(_player.MovementVector.x) <= 0.5f)
        {
            speed = _player.WalkSpeed;
        }
        else
        {
            speed = 0f;
        }

        return speed;
    }
}
