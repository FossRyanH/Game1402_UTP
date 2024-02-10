using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    PlayerController _player;

    float _currentSpeed;
    float _rotationDamping = 0.15f;

    public PlayerMoveState(PlayerController player)
    {
        this._player = player;
    }

    public void EnterState()
    {
        Debug.Log("Move State Entered");
        _currentSpeed = _player.RunSpeed;
    }

    public void UpdateState()
    {
        Vector3 movement = HandleMovement();
        HandleRotation();
        Move(movement);
    }

    Vector3 HandleMovement()
    {
        Vector3 motion = new Vector3();
        motion.x = _player.MovementVector.x;
        motion.y = 0f;
        motion.z = _player.MovementVector.y;

        Vector3 forwardDir = _player.CameraFocusPoint.forward;
        Vector3 rightDir = _player.CameraFocusPoint.right;

        forwardDir.y = 0f;
        rightDir.y = 0f;

        return forwardDir * motion.z + rightDir * motion.x;
    }

    void Move(Vector3 inputVector)
    {
        _player.Rb.velocity = inputVector * _currentSpeed;
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
}
