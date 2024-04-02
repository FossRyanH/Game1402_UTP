using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // Called once the character enters the state in question.
    public void EnterState();
    // Updates the State every frame based on delta
    public void UpdateState(float delta);
    // Called upon exiting the state in question.
    public void ExitState();

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
