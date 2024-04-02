using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using JetBrains.Annotations;

public class ForceReciever : MonoBehaviour
{
    float _verticalVelocity;
    float _drag = 0.3f;
    Vector3 _impact;
    Vector3 _dampingVelocity;

    #region Components
    NavMeshAgent _agent;
    CharacterController _controller;
    #endregion

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_verticalVelocity < 0f && _controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y *  Time.fixedDeltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);

        if (_agent != null)
        {
            if (_impact.sqrMagnitude < Mathf.Pow(0.2f, 2))
            {
                _impact = Vector3.zero;
                _agent.enabled = true;
            }
        }
    }

    public void Reset()
    {
        _impact = Vector3.zero;
        _verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
        if (_agent != null)
        {
            _agent.enabled = false;
        }
    }
}
