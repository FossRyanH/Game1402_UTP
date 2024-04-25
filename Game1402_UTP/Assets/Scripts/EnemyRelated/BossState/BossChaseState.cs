using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    private readonly int _bossChaseHash = Animator.StringToHash("RunForwardBattle");
    private float _animatorDampTime = 0.1f;
    
    // Start is called before the first frame update
    public BossChaseState(Boss boss) : base(boss)
    {
    }
    
    public override void EnterState()
    {
        if (_boss.IsInPhaseOne)
            _boss.MoveSpeed = _boss.WanderSpeed;
        else if (_boss.IsInPhaseTwo)
            _boss.MoveSpeed = _boss.ChargeSpeed;
        
        _boss.Animator.CrossFadeInFixedTime(_bossChaseHash, _animatorDampTime);
    }

    public override void UpdateState(float delta)
    {
        if (_boss.IsInPhaseOne)
        {
            if (!IsInChaseRange(_boss.PhaseOneChaseRange))
            {
                _boss.stateMachine.TransitionTo(new BossWanderState(_boss));
                return;
            }
            else if (IsInAttackRange(_boss.PlayerAttackRange))
            {
                _boss.stateMachine.TransitionTo(new BossAttackState(_boss));
            }
        }
        else if (_boss.IsInPhaseTwo)
        {
            if (!IsInChaseRange(_boss.PhaseTwoChaseRange))
            {
                _boss.stateMachine.TransitionTo(new BossWanderState(_boss));
                return;
            }
            else if (IsInAttackRange(_boss.PlayerAttackRange))
            {
                _boss.stateMachine.TransitionTo(new BossAttackState(_boss));
            }
        }

        if (Vector3.Distance(_boss.transform.position, _boss.Player.transform.position) > _boss.PlayerAttackRange)
        {
            MoveToPlayer(delta);
        }

        FacePlayer();
    }

    public override void ExitState()
    {
        _boss.Agent.ResetPath();
        _boss.Agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float delta)
    {
        if (_boss.Agent.isOnNavMesh)
        {
            _boss.Agent.destination = _boss.Player.transform.position;
            Move(_boss.Agent.desiredVelocity.normalized * _boss.MoveSpeed, delta);
        }

        _boss.Agent.velocity = _boss.Controller.velocity;
    }
}
