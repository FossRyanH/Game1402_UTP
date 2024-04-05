using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Pickup()
    {
        inventoryManager.Add(item);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pickup();
            Destroy(this.gameObject);
        }
    }

}
