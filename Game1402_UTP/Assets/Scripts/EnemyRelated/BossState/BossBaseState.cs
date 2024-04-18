using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : IState
{
    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void UpdateState(float delta) {}
}
