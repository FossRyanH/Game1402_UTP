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

    private int _damage;
    private float _knockback;

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
            health.TakeDamage(_damage);
        }

        if (other.TryGetComponent<ForceReciever>(out ForceReciever forceReciever))
        {
            Vector3 directionalForce = (other.transform.position - _characterCollider.transform.position).normalized;
            forceReciever.AddForce(directionalForce * _knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this._damage = damage;
        this._knockback = knockback;
    }
}
