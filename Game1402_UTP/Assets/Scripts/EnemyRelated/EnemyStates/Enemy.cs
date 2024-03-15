using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Components
    public Health EnemyHealth;
    public EnemyStateMachine StateMachine;
    public Target Target { get; private set; }
    #endregion

    private void Awake()
    {
        EnemyHealth = GetComponent<Health>();
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
        StateMachine.InitializeState(StateMachine.EnemyIdleState);
    }

    void HandleDeath()
    {
        StateMachine.TransitionTo(StateMachine.EnemyDeathState);
    }
}
