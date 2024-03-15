using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Events
    public event Action OnDie;
    #endregion
    
    [SerializeField] 
    private int _maxHealth = 100;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

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
        Destroy(this.gameObject, 0.25f);
    }
}
