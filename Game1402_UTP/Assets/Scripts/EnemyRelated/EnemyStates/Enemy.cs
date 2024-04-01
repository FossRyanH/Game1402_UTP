using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Components
    public Health EnemyHealth;
    public EnemyStateMachine StateMachine;
    public Target Target { get; private set; }
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public CharacterController Controller { get; private set; }
    [field: SerializeField]
    public ForceReciever ForceReciever { get; private set; }
    [field:SerializeField]
    public NavMeshAgent Agent { get; private set; }
    #endregion

    #region Combat Related
    [field: SerializeField]
    public float PlayerChaseRange { get; private set; } = 7.5f;
    #endregion

    #region Movement Variables
    [field: SerializeField]
    public float MoveSpeed = 0.5f;
    #endregion

    // Find Reference to Player
    [field: SerializeField]
    public PlayerController Player { get; private set; }

    private void Awake()
    {
        EnemyHealth = GetComponent<Health>();
        Controller = GetComponent<CharacterController>();
        ForceReciever = GetComponent<ForceReciever>();

        StateMachine = new EnemyStateMachine(this);
    }

    private void OnEnable()
    {
        EnemyHealth.OnDie += HandleDeath;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDie -= HandleDeath;
    }

    private void Start()
    {
        Player = FindFirstObjectByType<PlayerController>();

        StateMachine.InitializeState(StateMachine.EnemyIdleState);
    }

    void HandleDeath()
    {
        StateMachine.TransitionTo(StateMachine.EnemyDeathState);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChaseRange);
    }
}