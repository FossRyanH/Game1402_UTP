using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    public BossAttackState(Boss boss) : base(boss)
    {
        this._boss = boss;
    }
}
