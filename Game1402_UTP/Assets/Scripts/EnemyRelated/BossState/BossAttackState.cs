using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    private float _attackStateCoolDown;
    private bool _hasAttacked;

    private readonly int _primaryAttackHash = Animator.StringToHash("Attack02");
    private readonly int _bigAttackHash = Animator.StringToHash("Attack01");
    private float _animatorDampTime = 0.1f;

    private int _primaryAttackDamage = 15;
    private int _bigAttackDamage = 25;
    private float _knockbackDistance;
    
    public BossAttackState(Boss boss) : base(boss)
    {
        this._boss = boss;
    }

    public override void EnterState()
    {
        if (!_hasAttacked)
        {
            _hasAttacked = true;
            PhaseBasedAttack();
            _boss.StartCoroutine(AttackCoolDown());
        }
    }

    public override void UpdateState(float delta)
    {
        Move(delta);

        if (!_hasAttacked)
        {
            _boss.StateMachine.TransitionTo(new BossWanderState(_boss));
        }
    }

    public override void ExitState()
    {
        _boss.CanAttack = true;
    }

    void PhaseBasedAttack()
    {
        if (_boss.IsInPhaseTwo && _boss.CanAttack)
        {
            _attackStateCoolDown = 2f;
            _knockbackDistance = 2f;
            _boss.MoveSpeed = _boss.ChargeSpeed;
            _boss.Weapon.SetAttack(_bigAttackDamage, _knockbackDistance);
            _boss.Animator.CrossFadeInFixedTime(_bigAttackHash, _animatorDampTime);
        }
        else if (_boss.IsInPhaseOne && _boss.CanAttack)
        {
            _attackStateCoolDown = 1.5f;
            _knockbackDistance = 0.9f;
            _boss.MoveSpeed = _boss.WanderSpeed;
            _boss.Weapon.SetAttack(_primaryAttackDamage, _knockbackDistance);
            _boss.Animator.CrossFadeInFixedTime(_primaryAttackHash, _animatorDampTime);
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(_attackStateCoolDown);
        _hasAttacked = false;
    }
}
