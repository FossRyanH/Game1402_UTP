using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public GameObject ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }
    public void Add (Item item)
    {
        Items.Add(item);
    }
    public void Remove (Item item) 
    {
        Items.Remove(item);
    }
    public void ListItems()
    {

        // Cleans the content before opening it 

        GameObject lastObject = null;
        foreach (var  item in Items) 
        {
            GameObject obj = Instantiate(ItemContent, ItemContent.transform.parent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
           // var removeButton = obj.transform.Find("RemoveButton").gameObject.GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                //removeButton.gameObject.SetActive(true);
            }
            lastObject = obj;

        }
        if(Items.Count > 0)
        {
            Destroy(ItemContent);
            ItemContent = lastObject;
        }
     

        SetInventoryItems();
        //Destroy(ItemContent);

    }

    public void EnableItemsRemove() 
    { 
        if (EnableRemove.isOn) 
        { 
            //foreach (Transform item in ItemContent)
            //{
            //    item.Find("RemoveButton").gameObject.SetActive(true);
            //}
        }
        else
        {
            //foreach (Transform item in ItemContent)
            //{
            //    item.Find("RemoveButton").gameObject.SetActive(false);
            //}
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
