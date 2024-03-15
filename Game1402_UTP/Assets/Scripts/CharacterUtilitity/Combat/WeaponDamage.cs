using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField]
    private Collider _characterCollider;

    private List<Collider> _justHit = new List<Collider>();

    private void OnEnable()
    {
        _justHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _characterCollider)
        {
            return;
        }

        if (_justHit.Contains(other))
        {
            return;
        }
        
        _justHit.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(new AttackData().SwordDamage);
        }
    }
}
