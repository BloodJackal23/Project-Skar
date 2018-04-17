using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    public enum itemType { Consumable, KeyItem, Weaponry, Currency};
    public itemType type;
    public int itemCount = 1;

    //UI variables
    public string itemName;
    public Image itemImage;
}
