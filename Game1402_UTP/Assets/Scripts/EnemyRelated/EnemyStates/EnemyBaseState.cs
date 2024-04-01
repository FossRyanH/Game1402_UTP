using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected Enemy _enemy;
    
    public virtual void EnterState() {}
    
    public virtual void UpdateState(float delta) {}

    public virtual void ExitState() {}

    protected void Move(float delta)
    {
        Move(Vector3.zero, delta);
    }

    protected void Move(Vector3 motion, float delta)
    {
        _enemy.Controller.Move((motion + _enemy.ForceReciever.Movement) * delta);
    }

    protected void FacePlayer()
    {
        if (_enemy.Player == null) { return; }

        Vector3 lookPos = _enemy.Player.transform.position - _enemy.transform.position;
        lookPos.y = 0f;

        _enemy.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    // This method checks to see if the player is within the chasing range of the enemy.
    protected virtual bool IsInChaseRange()
    {
        float distanceToPlayerSqr = (_enemy.Player.transform.position - _enemy.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= Mathf.Pow(_enemy.PlayerChaseRange, 2);
    }
}
