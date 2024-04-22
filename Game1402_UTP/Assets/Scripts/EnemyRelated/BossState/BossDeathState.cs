using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    public BossDeathState(Boss boss) : base(boss)
    {
        this._boss = boss;
    }
}
