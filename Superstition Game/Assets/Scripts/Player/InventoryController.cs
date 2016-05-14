using UnityEngine;
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

    public ItemBase equippedMainHandItem;
    public ItemBase equippedOffHandItem;

    public List<ItemSlot> itemsInInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//ADD TO INVENTORY
    public void AddItemToInventory(ItemBase itemToAdd) {

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
        if (equippedMainHandItem != null && equippedMainHandItem == itemToRemove) {
            PutAwayCurrentItem(Hand.Main);
        }
        if (equippedOffHandItem != null && equippedOffHandItem == itemToRemove) {
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
                if (equippedMainHandItem != null) {
                    PutAwayCurrentItem(Hand.Main);
                }
                equippedMainHandItem = slotWithItem.item;

                break;


            case Hand.Off:

                if (equippedOffHandItem != null) {
                    PutAwayCurrentItem(Hand.Off);
                }
                equippedOffHandItem = slotWithItem.item;


                break;
        }
    }

//REMOVE ITEM FROM HAND, BUT KEEP IN INVENTORY
    public void PutAwayCurrentItem(Hand handToFreeUp) {

        switch (handToFreeUp) {

            case Hand.Main:
                if (equippedMainHandItem != null) {

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
                if (equippedMainHandItem != null) {
                    equippedMainHandItem.UseItem();
                }

                break;

            case Hand.Off:
                if (equippedOffHandItem != null) {
                    equippedOffHandItem.UseItem();
                }

                break;
        }
    }
}
