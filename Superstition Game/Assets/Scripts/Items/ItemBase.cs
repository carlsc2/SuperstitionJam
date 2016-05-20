using UnityEngine;
using System.Collections;

public class ItemBase : MonoBehaviour {
    [HideInInspector]
    public CharacterPawn owner;

    public enum UsageType {
        Timed,
        Toggle,
        
    }

    [SerializeField]
    private UsageType itemUsageType;


    public string id;

    public bool canInteruptUse = false;

    public bool isUsingItem;

    public float useTime;
    private Coroutine usingItemCoroutine = null;
    //timer countdown funcitonality for EndUseItem()
    private IEnumerator UseItemTimer(float numSec) {

        while (numSec > 0.0f) {
            numSec -= Time.deltaTime;

            yield return null;
        }

        //Debug.Log("end use", this);
        EndUseItemInternal();

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
        if (owner == null) {
            Debug.LogWarningFormat(this, "[{0}] Has No Owner. Cannot Perform BeginUseItem()", gameObject.name);
            return; }

        isUsingItem = true;

        switch (itemUsageType) {
            case UsageType.Timed:
                BeginTimedUseItem();
                break;

            case UsageType.Toggle:

                break;
        }

    }

    protected virtual void BeginTimedUseItem() {
        if (canInteruptUse && usingItemCoroutine != null) {
            StopCoroutine(usingItemCoroutine);
        }

        usingItemCoroutine = StartCoroutine(UseItemTimer(useTime));
    }

    protected virtual void BeginToggleUseItem() {
        
    }

    public virtual void EndUseItem() {

        Debug.Log("end Use Item");
        
        switch(itemUsageType) {
            case UsageType.Timed:
                //owner.EndUseItemByInstance(this);
                break;

            case UsageType.Toggle:
                EndUseItemInternal();
                break;
        }
        

        //owner.EndUseItemInHand();
    }

    protected virtual void EndUseItemInternal() {
        if (owner == null) { return; }

        isUsingItem = false;


        owner.EndUseItemByInstance(this);
    }

    public virtual void EnableItem() {
        gameObject.SetActive(true);
    }

    public virtual void DisableItem() {
        gameObject.SetActive(false);
    }

}
