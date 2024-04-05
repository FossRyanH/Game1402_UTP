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

    public PlayerInputManager InputManager;
    #endregion

    #region PlayerMovement Variables
    [Header("Motion Related Variables")]
    [SerializeField]
    public Vector2 MovementVector = new Vector2();
    [field: SerializeField]
    public float MoveSpeed;
    [SerializeField]
    public float WalkSpeed = 0.5f;
    [SerializeField]
    public float RunSpeed = 1.1f;
    [SerializeField]
    public float FallForce = 2.5f;
    [SerializeField]
    public float DodgeTimer = 0.5f;
    [SerializeField]
    public float DodgeSpeed = 2f;
    #endregion

    #region Misc Variables
    [SerializeField]
    float _interactDistance = 1.5f;

    #endregion

    #region Combat Variables
    [field: Header("Combat Related Variables")]
    [field: SerializeField]
    public AttackData[] Attack { get; private set; }
    [field: SerializeField]
    public WeaponDamage Weapon;
    public bool IsBlocking =  false;
    [SerializeField]
    public float DodgeLength = 2f;
    [SerializeField]
    public float DodgeDuration = 1.25f;
    #endregion

    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Force = GetComponent<ForceReciever>();
        Targeter = GetComponentInChildren<Targeter>();
        Health = GetComponent<Health>();

        StateMachine = new PlayerStateMachine();
        Animator = GetComponent<Animator>();
        InputManager = GetComponent<PlayerInputManager>();
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
        StateMachine.InitializeState(new PlayerLocomotionState(this));
    }

    void Update()
    {
        StateMachine.Update();
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

    public void HandleDeath()
    {
        StateMachine.TransitionTo(new PlayerDeathState(this));
    }
}
