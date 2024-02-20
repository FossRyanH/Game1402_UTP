using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    float _currentSpeed;
    float _rotationDamping = 0.15f;

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
        if (movement == Vector3.zero)
        HandleRotation();
        {
            _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }
    }


    Vector3 HandleMovement()
    {
        Vector3 motion = new Vector3();
        motion.x = _player.MovementVector.x;
        motion.z = _player.MovementVector.y;
        motion.y = 0f;


        Vector3 forwardDir = _player.CameraFocusPoint.forward;
        Vector3 rightDir = _player.CameraFocusPoint.right;

        forwardDir.y = 0f;
        rightDir.y = 0f;

        return forwardDir * motion.z + rightDir * motion.x;
    }

    void Move(Vector3 inputVector)
    {
        _player.Rb.velocity = inputVector * MarkSpeed(_currentSpeed);
    }

    void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = _player.CameraFocusPoint.forward * _player.MovementVector.y;
        targetDir.Normalize();
        targetDir.y = 0f;

        if (targetDir == Vector3.zero)
            targetDir = _player.transform.forward;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(_player.transform.rotation, targetRotation, _rotationDamping * Time.deltaTime);

        _player.transform.rotation = playerRotation;
    }

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
