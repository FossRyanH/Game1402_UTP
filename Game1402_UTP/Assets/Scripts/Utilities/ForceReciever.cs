using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    float _verticalVelocity;
    float _drag = 0.3f;
    Vector3 _impact;
    Vector3 _dampingVelocity;
    PlayerController _player;
    CharacterController _controller;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    void Awake()
    {
        _player = GetComponent<PlayerController>();
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GravityApplication();
    }

    void GravityApplication()
    {
        if (_verticalVelocity < 0f && _controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * (_player.FallForce - 1) *  Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }
}
