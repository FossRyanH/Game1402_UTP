using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Events
    public event Action OnDie;
    public event Action OnTakeDamage;
    #endregion
    
    [SerializeField] 
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth;

    public bool IsDead => _currentHealth == 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth == 0)
        {
            return;
        }
        
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        OnTakeDamage?.Invoke();

        if (_currentHealth == 0)
        {
            OnDie?.Invoke();
        }
        
        Debug.Log(_currentHealth);
    }

    public void RecoverHealth(int amount)
    {
        if (_currentHealth == _maxHealth)
        {
            return;
        }
        _currentHealth = Mathf.Max(_currentHealth + amount, _maxHealth);
    }

    // This function is for debug purposes only.
    public void Death()
    {
        Destroy(this.gameObject, 0.3f);
    }
}
