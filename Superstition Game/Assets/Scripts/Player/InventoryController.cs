using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[DisallowMultipleComponent]
public class InventoryController : MonoBehaviour {

    public GameObject[] startingItemsInInventory;

    //public SpriteRigController rig;

    //public string mainHandSocket;
    //public string offHandSocket;
    
    public enum Hand {
        Main = 0,
        Off = 1,
        None = 2,
    }
    

    [System.Serializable]
    public class ItemSlot {
        public ItemBase item;

        public int hotbarSlotIndex = -1;

        public ItemSlot(ItemBase item) {
            this.item = item;
        } 

        public ItemSlot(ItemBase item, int hotbarSlotIndex) {
            this.item = item;
            this.hotbarSlotIndex = hotbarSlotIndex;
        }
    }

    //public ItemBase mainHandItem;
    //public ItemBase offHandItem;

    public List<ItemSlot> itemsInInventory;


    public ItemBase CreateAndAddToInventory(GameObject itemToAdd, CharacterPawn owningPawn) {
        ItemBase itemComp = itemToAdd.GetComponent<ItemBase>();

        if (itemComp == null) { return null; }

        GameObject newGo = (GameObject)GameObject.Instantiate(itemToAdd, transform.position, Quaternion.identity);

        newGo.transform.SetParent(transform, false);
        AddItemToInventory(newGo.GetComponent<ItemBase>(), owningPawn);

        //need to return the new object's component b/c it's not a prefab
        return newGo.GetComponent<ItemBase>();

    }

	// Use this for initialization
	void Awake () {


        /*
        ItemSlot[] invCopy = new ItemSlot[itemsInInventory.Count];
        itemsInInventory.CopyTo(invCopy);
        itemsInInventory = new List<ItemSlot>();
        */

        /*
        foreach (ItemSlot slot in invCopy) {
            AddItemToInventory(slot.item);
        }

	    if (mainHandItem != null) {
            AddItemToInventory(mainHandItem);

            mainHandItem.EnableItem();
            PullOutItem(mainHandItem, Hand.Main);
        } 

        if (offHandItem != null) {
            AddItemToInventory(offHandItem);

            offHandItem.EnableItem();
            PullOutItem(offHandItem, Hand.Off);
        }
        */
	}
	
    public void Init(CharacterPawn owningPawn) {
        itemsInInventory = new List<ItemSlot>();

        foreach (GameObject go in startingItemsInInventory) {
            CreateAndAddToInventory(go, owningPawn);
        }

    }


    //ADD TO INVENTORY
    public void AddItemToInventory(ItemBase itemToAdd, CharacterPawn owningPawn) {

        if (itemToAdd == null) { return; }

        //assign owner b/c the check only sees if we already have this item
        itemToAdd.owner = owningPawn;

        itemToAdd.transform.parent = transform;

        itemToAdd.DisableItem();

        //don't try to add unless we already have this item
        if (itemsInInventory.Select(x => x.item).Contains(itemToAdd)) { return; }

        int hotbarSlot = -1;
        if (itemToAdd as SwordItem != null) { ++hotbarSlot; }

        foreach (ItemSlot slot in itemsInInventory) {
            if (slot.item as SwordItem != null) {
                ++hotbarSlot;
            }
        }

        itemsInInventory.Add(new ItemSlot(itemToAdd, hotbarSlot));

    }

//REMOVE FROM INVENTORY
    public void RemoveItemFromInventory(ItemBase itemToRemove) {

        //make sure the item exists in the inventory first
        //if (!itemsInInventory.Select(x => x.item).Contains(itemToRemove)) {
        if (!HasItem(itemToRemove)) {

            Debug.LogErrorFormat(this, "Item {0} does not exist in Inventory, cannot remove", itemToRemove.name);

            return;
        }
        /*
        //check to see if the item we want to remove is in one of the hands
         //put away first if so
        if (mainHandItem != null && mainHandItem == itemToRemove) {
            PutAwayCurrentItem(Hand.Main);
        }
        if (offHandItem != null && offHandItem == itemToRemove) {
            PutAwayCurrentItem(Hand.Off);
        }
        */
        //now we can safely remove the item
        itemsInInventory.Remove(itemsInInventory.First(x => x.item == itemToRemove));

    }
    /*
//PLACE ITEM IN HAND
    public void PullOutItem(ItemBase item, Hand handToPutIn) {
        if (item == null || !HasItem(item)) { return; }

        //item.EnableItem();

        switch (handToPutIn) {

            case Hand.Main:
                if (mainHandItem != null) {
                    PutAwayCurrentItem(Hand.Main);
                }

                mainHandItem = item;
                item.EnableItem();

                rig.AttachObjectToSocket(item.transform, mainHandSocket);

                break;


            case Hand.Off:

                if (offHandItem != null) {
                    PutAwayCurrentItem(Hand.Off);
                }
                offHandItem = item;

                item.EnableItem();
                rig.AttachObjectToSocket(item.transform, offHandSocket);

                break;
        }
    }

//REMOVE ITEM FROM HAND, BUT KEEP IN INVENTORY
    public void PutAwayCurrentItem(Hand handToFreeUp) {

        switch (handToFreeUp) {

            case Hand.Main:
                if (mainHandItem == null) { break; }

                mainHandItem.DisableItem();
                mainHandItem = null;
                
                break;


            case Hand.Off:
                if (offHandItem == null) { break; }

                offHandItem.DisableItem();
                offHandItem = null;

                break;
        }
    }

//USE ITEM IN SUPPLIED HAND
    public void BeginUseItemInHand(Hand handWithItem) {

        switch (handWithItem) {

            case Hand.Main:
                if (mainHandItem == null) { break; }

                mainHandItem.BeginUseItem();

                break;

            case Hand.Off:
                if (offHandItem == null) { break; }

                offHandItem.BeginUseItem();

                break;
        }
    }

    public void EndUseItemInHand(Hand handWithItem) {

        switch (handWithItem) {

            case Hand.Main:
                if (mainHandItem == null) { break; }

                mainHandItem.EndUseItem();

                break;

            case Hand.Off:
                if (offHandItem == null) { break; }

                offHandItem.EndUseItem();

                break;
        }
    }
    */

    public bool HasItem(ItemBase item) {
        return itemsInInventory.Select(x => x.item).Contains(item);
    }

    public bool HasItemOfId(string id) {
        foreach (ItemSlot slot in itemsInInventory) {
            if (slot.item.id == id) {
                return true;
            }
        }

        return false;
    }

    public void RemoveFirstItemOfId(string id) {
        foreach (ItemSlot slot in itemsInInventory) {
            if (slot.item.id == id) {

                RemoveItemFromInventory(slot.item);

                break;
            }
        }
    }
    /*
    public void EquipHotbarItem(int hotbarSlotIndex, Hand equipHand = Hand.Main) {
        foreach (ItemSlot slot in itemsInInventory) {
            if (slot.hotbarSlotIndex == hotbarSlotIndex) {
                PullOutItem(slot.item, equipHand);
                break;
            }
        }
    }
    */
}
