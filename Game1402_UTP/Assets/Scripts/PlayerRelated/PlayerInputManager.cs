using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    PlayerController _playerController;
    PlayerActions _inputActions;

    void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerActions();
            _inputActions.PlayerMovement.Move.performed += i => _playerController.HandleMovement(i.ReadValue<Vector2>());
            // _inputActions.PlayerAction.Jump.performed += i => _playerController.HandleJumpInput();
        }
        _inputActions.Enable();
    }

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
}
