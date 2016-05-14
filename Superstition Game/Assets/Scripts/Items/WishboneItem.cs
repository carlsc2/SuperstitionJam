using UnityEngine;
using System.Collections;

public class WishboneItem : ItemBase{

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player")
        {
            owner = col.GetComponent<InventoryController>();
            owner.AddItemToInventory(this);

        }
    
    }
}
