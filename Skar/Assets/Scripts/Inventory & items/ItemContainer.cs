using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour {
    public bool locked;
    public List<InventoryItem> containedItems = new List<InventoryItem>();

    private void Start()
    {
        InventoryItem[] items = GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem item in items)
        {
            containedItems.Add(item);
        }
    }
}
