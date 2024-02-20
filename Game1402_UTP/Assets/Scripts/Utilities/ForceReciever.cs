using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    float _verticalVelocity;
    PlayerController _player;

    void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_verticalVelocity < 0f && _player.IsGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * (_player.FallForce - 1) *  Time.deltaTime;
        }
    }
}
