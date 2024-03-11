using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected Enemy _enemy;
    
    public virtual void EnterState()
    {
    }
    
    public virtual void UpdateState(float delta)
    {
    }

    public virtual void ExitState()
    {
    }
}
