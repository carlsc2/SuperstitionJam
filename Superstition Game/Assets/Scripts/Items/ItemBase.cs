using UnityEngine;
using System.Collections;
using System;

public class ItemBase : MonoBehaviour {
    [HideInInspector]
    public InventoryController owner;

    public string id;

    protected virtual void Awake() {

    }

	// Use this for initialization
    protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}


    public virtual void EquipItem() {

    }

    public virtual void UnequipItem() {

    }

    public virtual void BeginUseItem() {

    }

    public virtual void EndUseItem() {

    }

    public virtual void EnableItem() {
        gameObject.SetActive(true);
    }

    public virtual void DisableItem() {
        gameObject.SetActive(false);
    }
}
