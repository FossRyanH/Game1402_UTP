using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _attackBox;

    void Awake()
    {
        _attackBox.SetActive(false);
    }

    public void EnableWeapon()
    {
        _attackBox.SetActive(true);
    }

    public void DisableWeapon()
    {
        _attackBox.SetActive(false);
    }
}
