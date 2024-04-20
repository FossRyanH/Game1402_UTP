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
    
    protected void FacePlayer()
    {
        if (_boss.Player == null) { return; }

        Vector3 lookPos = _boss.Player.transform.position - _boss.transform.position;
        lookPos.y = 0f;

        _boss.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
