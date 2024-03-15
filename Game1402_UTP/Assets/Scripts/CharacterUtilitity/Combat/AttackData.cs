using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackData
{
    [field: SerializeField]
    public string AttackName { get; private set; }
    [field: SerializeField]
    public float AttackTransition { get; private set; } = 0.1f;
    [field: SerializeField]
    public float AttackCooldown { get; private set; } = 0.65f;

    [field: SerializeField]
    public int SwordDamage { get; private set; } = 35;
}
