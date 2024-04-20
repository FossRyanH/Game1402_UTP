using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWanderState : BossBaseState
{
    public BossWanderState(Boss boss) : base(boss)
    {
        this._boss = boss;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState(float delta)
    {
        base.UpdateState(delta);
    }
}
