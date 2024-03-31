using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
    void OnCollisionEnter (Collision collision)
    {
        Pickup();
    }

}
