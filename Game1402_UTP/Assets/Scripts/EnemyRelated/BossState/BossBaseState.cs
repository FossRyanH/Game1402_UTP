using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : IState
{
    protected Boss _boss;

    public BossBaseState(Boss boss)
    {
        this._boss = boss;
    }
    
    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void UpdateState(float delta) {}

    protected void Move(Vector3 motion, float delta)
    {
        _boss.Controller.Move((motion + _boss.ForceReciever.Movement) * delta);
    }

    protected void Move(float delta)
    {
        Move(Vector3.zero, delta);
    }
    
    protected void FacePlayer()
    {
        if (_boss.Player == null) { return; }

        Vector3 lookPos = _boss.Player.transform.position - _boss.transform.position;
        lookPos.y = 0f;

        _boss.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    
    protected bool IsInChaseRange(float range)
    {
        float distanceToPlayerSqr = (_boss.Player.transform.position - _boss.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= Mathf.Pow(range, 2);
    }
    
    protected bool IsInAttackRange(float attackRange)
    {
        if (_boss.Player.IsDead)
        {
            return false;
        }
        
        float distanceToPlayerSqr = (_boss.Player.transform.position - _boss.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= Mathf.Pow(attackRange, 2);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
