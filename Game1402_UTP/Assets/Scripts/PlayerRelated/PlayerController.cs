    /*  */

    using System;
    using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    public CharacterController Controller;
    public ForceReciever Force;
    public Targeter Targeter;
    public Animator Animator { get; private set; }
    public PlayerStateMachine StateMachine;
    [SerializeField]
    public Transform CameraFocusPoint;
    public Health Health { get; private set; }
    #endregion

    #region PlayerMovement Variables
    [Header("Motion Related Variables")]
    [SerializeField]
    public Vector2 MovementVector = new Vector2();
    [SerializeField]
    public float WalkSpeed = 0.5f;
    [SerializeField]
    public float RunSpeed = 2f;
    [SerializeField]
    public float FallForce = 2.5f;
    [SerializeField]
    public float DodgeTimer = 0.5f;
    #endregion

    #region Misc Variables
    [SerializeField]
    float _interactDistance = 1.5f;

    public bool IsTargeting = false;
    #endregion

    #region Combat Variables
    [field: Header("Combat Related Variables")]
    [field: SerializeField]
    public AttackData Attack { get; private set; }
    public bool IsAttacking = false;
    public bool CanAttack = true;
    [SerializeField]
    public float DodgeLength = 2f;
    [SerializeField]
    public float DodgeDuration = 1.25f;
    #endregion
    
    #region ConditionalEvents
    public event Action CancelEvent;
    public event Action TargetEvent;
    #endregion

    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Force = GetComponent<ForceReciever>();
        Targeter = GetComponentInChildren<Targeter>();
        Health = GetComponent<Health>();

        StateMachine = new PlayerStateMachine(this);
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Health.OnDie += HandleDeath;
    }

    private void OnDisable()
    {
        Health.OnDie -= HandleDeath;
    }

    void Start()
    {
        StateMachine.InitializeState(StateMachine.LocomotionState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    // Handle the vector input of the player, to move (or not) the player to teh MoveState.
    public void HandleMovement(Vector2 input)
    {
        MovementVector = input;
    }

    public void ProcessAttack(bool isAttacking)
    {
        // Will only attack if the input for attack is trigger *and* the player has the ability to perform the attack.
        if (isAttacking && CanAttack)
        {
            StateMachine.TransitionTo(StateMachine.AttackState);
        }
    }

    public void HandleTargeting()
    {
        TargetEvent?.Invoke();
    }

    // Handles the player interacting with doors, chests..etc
    public void Interaction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            Interact(hit.collider);
        }
    }

    void Interact(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            other.GetComponent<Interactable>().Interact();
        }
    }

    public void OnCancel()
    {
        CancelEvent?.Invoke();
    }

    void HandleDeath()
    {
        StateMachine.TransitionTo(StateMachine.DeathState);
    }
}
