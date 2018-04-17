using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int currency;

    public List<InventoryItem> consumables = new List<InventoryItem>();
    public List<InventoryItem> keyItems = new List<InventoryItem>();
    public List<InventoryItem> weaponry = new List<InventoryItem>();

    InventoryItem existingItem;

    public void AddToInventory(InventoryItem newItem)
    {
        switch (newItem.type)
        {
            case InventoryItem.itemType.Consumable:
                CheckForExistingItem(consumables, newItem, false);
                break;

            case InventoryItem.itemType.Currency:
                currency += newItem.itemCount;
                break;

            case InventoryItem.itemType.KeyItem:
                CheckForExistingItem(keyItems, newItem, true);
                break;

            case InventoryItem.itemType.Weaponry:
                CheckForExistingItem(weaponry, newItem, true);
                break;
        }
    }

    void CheckForExistingItem(List<InventoryItem> items, InventoryItem addedItem, bool unique)
    {
        if(!unique)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemName == addedItem.itemName)
                {
                    existingItem = items[i];
                    i = items.Count;
                }
                else
                {
                    existingItem = null;
                }
            }

            if (existingItem != null)
            {
                existingItem.itemCount += addedItem.itemCount;
            }
            else
            {
                items.Add(addedItem);
            }
        }
        else
        {
            addedItem.itemCount = 1;
            items.Add(addedItem);
        }
    }
}
