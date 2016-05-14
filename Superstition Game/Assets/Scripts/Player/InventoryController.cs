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

    public ItemSlot equippedMainHandItem;

    public List<ItemSlot> itemsInInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddItemToInventory(ItemBase itemToAdd) {

        itemsInInventory.Add(new ItemSlot(itemToAdd));
        
    }

    public void RemoveItemFromInventory(ItemBase itemToRemove) {
        if (!itemsInInventory.Select(x => x.item).Contains(itemToRemove)) {

            Debug.LogErrorFormat(this, "Item {0} does not exist in Inventory, cannot remove", itemToRemove.name);

            return;
        }

        itemsInInventory.Remove(itemsInInventory.First(x => x.item == itemToRemove));

    }

    public void PullOutItem(ItemSlot slotWithItem, Hand handToPutIn) {
        if (slotWithItem.item == null) { return; }

        //equippedMainHandItem
    }

    public void PutAwayCurrentItem() {
        //if ()
    }
}
