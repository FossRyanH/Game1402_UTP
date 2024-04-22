using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    private float _attackStateCoolDown;
    private float _previousFrameTime;
    private AttackData _attack;
    
    public BossAttackState(Boss boss, int attackIndex) : base(boss)
    {
        this._boss = boss;
        _attack = _boss.Attack[attackIndex];
    }

    public override void EnterState()
    {
        _boss.Weapon?.SetAttack(_attack.Damage, _attack.KnockbackDistance);
        _boss.Animator.CrossFadeInFixedTime(_attack.AttackName, _attack.AttackTransition);
        if (_boss.IsInPhaseTwo)
        {
            _attackStateCoolDown = 2f;
            _boss.StateMachine.TransitionTo(new BossAttackState(_boss, 0));
        }
        else
        {
            _attackStateCoolDown = 0.75f;
        }
    }

    public override void UpdateState(float delta)
    {
        Move(delta);
        _attackStateCoolDown -= Time.fixedDeltaTime;
        if (_attackStateCoolDown <= 0f)
        {
            _boss.StateMachine.TransitionTo(new BossWanderState(_boss));
        }
    }
}
