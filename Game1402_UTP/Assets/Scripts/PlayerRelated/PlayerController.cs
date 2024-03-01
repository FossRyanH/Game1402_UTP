    /*  */using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    public CharacterController Controller;
    public ForceReciever Force;
    [SerializeField]
    public Animator Animator { get; private set; }
    public PlayerStateMachine StateMachine;
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
    public float FallForce = 2.5f;
    [SerializeField]
    public float DodgeTimer = 0.5f;
    #endregion

    #region Misc Variables
    public bool IsAttacking = false;
    [SerializeField]
    public float DodgeLength = 2f;
    [SerializeField]
    public float DodgeDuration = 1.25f;
    [SerializeField]
    float _interactDistance = 0.75f;
    #endregion

    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Force = GetComponent<ForceReciever>();

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

    public void ProcessAttack(bool isAttacking)
    {
        if (isAttacking)
        {
            StateMachine.TransitionTo(StateMachine.AttackState);
        }
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
}
