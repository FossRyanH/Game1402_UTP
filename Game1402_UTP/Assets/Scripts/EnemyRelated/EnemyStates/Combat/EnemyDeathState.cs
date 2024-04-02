using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Enemy enemy)
    {
        this._enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Weapon.gameObject.SetActive(false);
        _enemy.EnemyHealth.Death();
    }
}
