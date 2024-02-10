using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    public Rigidbody Rb;
    [SerializeField]
    public Animator Animator { get; private set; }
    public PlayerStateMachine StateMachine;  
    SphereCollider _jumpCollider;
    [SerializeField]
    public Transform CameraFocusPoint;
    #endregion

    #region PlayerMovement Variables
    [Header("Motion Related Variables")]
    [SerializeField]
    public Vector2 MovementVector = new Vector2();
    [SerializeField]
    public float WalkSpeed = 1.25f;
    [SerializeField]
    public float RunSpeed = 5f;
    [SerializeField]
    public float JumpForce = 3.5f;

    bool _isGrounded = false;
    #endregion

    #region Misc Variables
    bool _isInCombat = false;
    #endregion

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _jumpCollider = GetComponent<SphereCollider>();
        _jumpCollider.isTrigger = true;

        StateMachine = new PlayerStateMachine(this);
        Animator = GetComponent<Animator>();
    }

    void Start()
    {
        StateMachine.InitState(StateMachine.IdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    // Handle the vector input of the player, to move (or not) the player to teh MoveState.
    public void HandleMovement(Vector2 input)
    {
        MovementVector = input;

        if (input == Vector2.zero) { return; }
        StateMachine.TransitionTo(StateMachine.MoveState);
    }

    // Processes the player jumping functionality to JumpState.
    public void HandleJumpInput()
    {
        if (_isGrounded)
        {
            StateMachine.TransitionTo(StateMachine.JumpState);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
            Debug.Log($"Grounded is {_isGrounded}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }

    // On this action will enter the player into combat state.
    public void IsInCombat(bool inCombat)
    {
        _isInCombat = inCombat;
        if (inCombat)
        {
            
        }
    }

}
