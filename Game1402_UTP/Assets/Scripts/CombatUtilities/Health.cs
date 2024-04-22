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
    
    [field: SerializeField] 
    public int MaxHealth { get; private set; } =  100;
    [field: SerializeField]
    public int CurrentHealth;

    public bool IsDead => CurrentHealth == 0;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    // Calls the TakeDamage on whoever its needed on to decrease player or enemy health
    public void TakeDamage(int damage)
    {
        if (CurrentHealth == 0) { return; }
        
        // Sets the health so it can never go below 0
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        OnTakeDamage?.Invoke();

        if (CurrentHealth == 0)
        {
            OnDie?.Invoke();
        }
        
        Debug.Log(CurrentHealth);
    }

    public void RecoverHealth(int amount)
    {
        if (CurrentHealth == MaxHealth)
        {
            return;
        }
        CurrentHealth = Mathf.Max(CurrentHealth + amount, MaxHealth);
    }

    // This function is for debug purposes only.
    public void Death()
    {
        Destroy(this.gameObject, 0.3f);
    }
}
