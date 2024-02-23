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

}
