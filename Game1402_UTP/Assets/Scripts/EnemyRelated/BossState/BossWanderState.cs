using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWanderState : BossBaseState
{
    private int _currentWanderPoint = 0;
    
    private readonly int _moveHash = Animator.StringToHash("WalkForwardBattle");
    public BossWanderState(Boss boss) : base(boss)
    {
        this._boss = boss;
    }

    public override void EnterState()
    {
        _boss.Animator.CrossFadeInFixedTime(_moveHash, Time.fixedDeltaTime);
        _boss.MoveSpeed = _boss.WanderSpeed;
    }

    public override void UpdateState(float delta)
    {
        FacePlayer();
        MoveToWanderPoint(delta);

        if (IsInRadius(_boss.Player.gameObject, _boss.PlayerAttackRange))
        {
            _boss.StateMachine.TransitionTo(new BossAttackState(_boss));
        }
        Move(delta);
    }
    
    void MoveToWanderPoint(float delta)
    {
        _boss.Target = _boss.WanderPoints[_currentWanderPoint];
        ChangeTarget(_boss.Target);
        
        if (_boss.Agent.isOnNavMesh)
        {
            _boss.Agent.destination = _boss.Target.transform.position;
            Move(_boss.Agent.desiredVelocity.normalized * _boss.MoveSpeed, delta);
            
            if (IsInRadius(_boss.WanderPoints[_currentWanderPoint], _boss.WanderRadius))
            {
                _currentWanderPoint++;
                _currentWanderPoint %= _boss.WanderPoints.Length;
            
                ChangeTarget(_boss.WanderPoints[_currentWanderPoint]);
            }
        }

        _boss.Agent.velocity = _boss.Controller.velocity;
    }
}
