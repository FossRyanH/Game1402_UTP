using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private List<string> items = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemId)
    {
        items.Add(itemId);
    }

    public List<string> GetInventory()
    {
        return items;
    }
}
