    /*  */using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    public float FallForce = 3.5f;

    public bool IsGrounded;
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
        StateMachine.InitializeState(StateMachine.IdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    // Handle the vector input of the player, to move (or not) the player to teh MoveState.
    public void HandleMovement(Vector2 input)
    {
        MovementVector = input;
        StateMachine.TransitionTo(StateMachine.MoveState);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
        Debug.Log($"Grounded is {IsGrounded}");
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
