﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryController : MonoBehaviour {

    public enum Hand {
        Main,
        Off,
    }

    [System.Serializable]
    public class ItemSlot {
        public ItemBase item;

        public int hotbarSlotIndex;

        public ItemSlot(ItemBase item) {
            this.item = item;
        } 
    }

    public ItemBase mainHandItem;
    public ItemBase offHandItem;

    public List<ItemSlot> itemsInInventory;

	// Use this for initialization
	void Start () {
	    if (mainHandItem != null) {
            AddItemToInventory(mainHandItem);
        } 

        if (offHandItem != null) {
            AddItemToInventory(offHandItem);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//ADD TO INVENTORY
    public void AddItemToInventory(ItemBase itemToAdd) {

        //assign owner b/c the check only sees if we already have this item
        itemToAdd.owner = this;

        //don't try to add unless we already have this item
        if (itemsInInventory.Select(x => x.item).Contains(itemToAdd)) { return; }

        itemsInInventory.Add(new ItemSlot(itemToAdd));

    }

//REMOVE FROM INVENTORY
    public void RemoveItemFromInventory(ItemBase itemToRemove) {

        //make sure the item exists in the inventory first
        if (!itemsInInventory.Select(x => x.item).Contains(itemToRemove)) {

            Debug.LogErrorFormat(this, "Item {0} does not exist in Inventory, cannot remove", itemToRemove.name);

            return;
        }

        //check to see if the item we want to remove is in one of the hands
         //put away first if so
        if (mainHandItem != null && mainHandItem == itemToRemove) {
            PutAwayCurrentItem(Hand.Main);
        }
        if (offHandItem != null && offHandItem == itemToRemove) {
            PutAwayCurrentItem(Hand.Off);
        }

        //now we can safely remove the item
        itemsInInventory.Remove(itemsInInventory.First(x => x.item == itemToRemove));

    }

//PLACE ITEM IN HAND
    public void PullOutItem(ItemSlot slotWithItem, Hand handToPutIn) {
        if (slotWithItem.item == null) { return; }

        switch (handToPutIn) {

            case Hand.Main:
                if (mainHandItem != null) {
                    PutAwayCurrentItem(Hand.Main);
                }
                mainHandItem = slotWithItem.item;

                break;


            case Hand.Off:

                if (offHandItem != null) {
                    PutAwayCurrentItem(Hand.Off);
                }
                offHandItem = slotWithItem.item;


                break;
        }
    }

//REMOVE ITEM FROM HAND, BUT KEEP IN INVENTORY
    public void PutAwayCurrentItem(Hand handToFreeUp) {

        switch (handToFreeUp) {

            case Hand.Main:
                if (mainHandItem != null) {

                }

                break;

            case Hand.Off:

                break;
        }
    }

//USE ITEM IN SUPPLIED HAND
    public void UseItemInHand(Hand handWithItem) {

        switch (handWithItem) {

            case Hand.Main:
                if (mainHandItem != null) {
                    mainHandItem.UseItem();
                }

                break;

            case Hand.Off:
                if (offHandItem != null) {
                    offHandItem.UseItem();
                }

                break;
        }
    }
}
