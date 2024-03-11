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
        Debug.Log("EnteredDeathState");
        _enemy.EnemyHealth.OnDie += EnemyDie;
    }

    void EnemyDie()
    {
        _enemy.EnemyHealth.Death();
    }
}
