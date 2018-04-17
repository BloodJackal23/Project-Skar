using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkarLib
{
    public static class Interactions
    {
        public enum interactableTypes { Container, Pickup, Switch, Door, Dialogue, Light };

        public static void ContainerAction(GameObject player, Interactable container)
        {
            Inventory inventory = player.GetComponentInChildren<Inventory>();
            ItemContainer itemContainer = container.GetComponentInChildren<ItemContainer>();
            if(!itemContainer.locked)
            {
                foreach (InventoryItem item in itemContainer.containedItems)
                {
                    inventory.AddToInventory(item);
                }
                itemContainer.containedItems.Clear();
            }
        }

        public static void SwitchAction(Interactable switchObject)
        {
            Switch switch_Access = switchObject.GetComponentInChildren<Switch>();
            
            if(switch_Access.on)
            {
                switch_Access.on = false;
            }
            else
            {
                switch_Access.on = true;
            }

            foreach (Interactable controlledObject in switch_Access.controlledObjects)
            {
                switch_Access.SwitchEffect(controlledObject);
            }
        }
    }
}