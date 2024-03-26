using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected Enemy _enemy;
    
    public virtual void EnterState()
    {
    }
    
    public virtual void UpdateState()
    {
    }

    public virtual void ExitState()
    {
    }

    // This method checks to see if the player is within the chasing range of the enemy.
    protected virtual bool IsInChaseRange()
    {
        float distanceToPlayerSqr = (_enemy.Player.transform.position - _enemy.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= Mathf.Pow(_enemy.PlayerChaseRange, 2);
    }
}
