using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public override void EnterState()
    {
        GameObject.Destroy(_enemy.Target);
        _enemy.EnemyHealth.Death();
    }
}
