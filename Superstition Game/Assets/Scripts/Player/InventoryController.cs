using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

    public List<ItemBase> itemsInInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddItemToInventory(ItemBase itemToAdd) {
        itemsInInventory.Add(itemToAdd);
    }

    public void RemoveItemFromInventory(ItemBase itemToRemove) {
        if (!itemsInInventory.Contains(itemToRemove)) {

            Debug.LogErrorFormat(this, "Item {0} does not exist in Inventory, cannot remove", itemToRemove.name);

            return;
        }

        itemsInInventory.Remove(itemToRemove);

    }
}
