using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    // Protected variable PlayerController is accessed to each State belonging to the player
    protected PlayerController _player;
    
    // Called upon entering every state
    public virtual void EnterState(){}

    // updates everything happening inside the state so long as the state is active multiplied by delta to make it framerate independant.
    public virtual void UpdateState(float delta) {}

    // Called once the state is being exited.
    public virtual void ExitState() {}

    protected void Move(float delta)
    {
        Move(Vector3.zero, delta);
    }

    protected void Move(Vector3 inputVector, float delta)
    {
        _player.Controller.Move((inputVector + _player.Force.Movement) * delta);
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

    protected void ReturnToLocomotion()
    {
        if (_player.Targeter.CurrentTarget != null)
        {
            _player.StateMachine.TransitionTo(new PlayerTargetingState(_player));
        }
        else
        {
            _player.StateMachine.TransitionTo(new PlayerLocomotionState(_player));
        }
    }

}
