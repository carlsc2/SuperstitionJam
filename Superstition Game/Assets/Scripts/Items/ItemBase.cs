using UnityEngine;
using System.Collections;
using System;

public class ItemBase : MonoBehaviour {
    [HideInInspector]
    //public InventoryController owner;
    public CharacterPawn owner;

    public string id;

    public bool canInteruptUse = false;

    public float useTime;
    private Coroutine usingItemCoroutine = null;
    //timer countdown funcitonality for EndUseItem()
    private IEnumerator UseItemTimer(float numSec) {

        while (numSec > 0.0f) {
            numSec -= Time.deltaTime;

            yield return null;
        }


        Debug.Log("end use", this);
        EndUseItem();

        yield break;
    }


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
        if (canInteruptUse && usingItemCoroutine != null) {
            StopCoroutine(usingItemCoroutine);
        }

        usingItemCoroutine = StartCoroutine(UseItemTimer(useTime));
    }

    public virtual void EndUseItem() {
        if (owner == null) { return; }

        owner.EndUseItemByInstance(this);
        //owner.EndUseItemInHand()
    }

    public virtual void EnableItem() {
        gameObject.SetActive(true);
    }

    public virtual void DisableItem() {
        gameObject.SetActive(false);
    }

}
