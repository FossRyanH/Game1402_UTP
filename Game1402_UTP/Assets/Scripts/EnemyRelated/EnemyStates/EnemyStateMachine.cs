using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine : StateMachine
{
    #region EnemyStates
    public EnemyDeathState EnemyDeathState;
    public EnemyIdleState EnemyIdleState;
    public EnemyChaseState EnemyChaseState;
    #endregion
    
    public EnemyStateMachine(Enemy enemy)
    {
        this.EnemyDeathState = new EnemyDeathState(enemy);
        this.EnemyIdleState = new EnemyIdleState(enemy);
        this.EnemyChaseState = new EnemyChaseState(enemy);
    }
}
