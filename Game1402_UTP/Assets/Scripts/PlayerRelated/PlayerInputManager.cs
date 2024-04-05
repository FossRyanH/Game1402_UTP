using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour, PlayerActions.IPlayerActionActions, PlayerActions.IPlayerMovementActions
{
    PlayerController _playerController;
    PlayerActions _inputActions;
    PauseMenu _pauseMenu;

    #region State Events
    public event Action DodgeEvent;
    public event Action TargetAction;
    public event Action CancelEvent;
    #endregion

    public bool IsAttacking;

     void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void OnEnable()
    {
        _inputActions = new PlayerActions();
        _inputActions.PlayerAction.SetCallbacks(this);
        _inputActions.PlayerMovement.SetCallbacks(this);
        _inputActions.Enable();
    }

    void OnDestroy()
    {
        _inputActions.PlayerAction.Disable();
        _inputActions.PlayerMovement.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        CancelEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        DodgeEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _playerController.Interaction();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _playerController.MovementVector = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        _pauseMenu.HandlePauseMenu();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetAction?.Invoke();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        // TODO: Add Inventory functionality
        if (!context.performed) { return; }
    }
}
