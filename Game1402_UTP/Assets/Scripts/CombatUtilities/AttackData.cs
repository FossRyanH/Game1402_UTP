using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackData
{
    #region Animation and Attack indexes
    [field: SerializeField]
    public string AttackName { get; private set; }
    [field: SerializeField]
    public float AttackTransition { get; private set; } = 0.1f;
    [field: SerializeField]
    public int ComboStateIndex { get; private set; } = -1;
    [field: SerializeField]
    public float ComboAttackTime { get; private set; }
    #endregion

    #region Damage and combat params
    [field: SerializeField]
    public float ForceTime { get; private set; }
    [field: SerializeField]
    public float Force { get; private set; }
    [field: SerializeField]
    public int Damage { get; private set; } = 35;
    [field: SerializeField]
    public float KnockbackDistance { get; private set; }
    #endregion
}
