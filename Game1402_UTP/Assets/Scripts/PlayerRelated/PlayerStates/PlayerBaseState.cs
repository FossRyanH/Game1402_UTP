using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerController _player;
    
    public virtual void EnterState(){}

    public virtual void ExitState() {}

    public virtual void UpdateState(float delta) {}
}
