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
}
