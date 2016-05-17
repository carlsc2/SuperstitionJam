using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CharacterPawn : Pawn {

    public enum Hand {
        Main = 0,
        Off = 1,
    }

    //public delegate void OnDeathCallback();
    //OnDeathCallback onPawnDeath;

    //public PlayerController owningController;

    public UnityEvent OnPawnDeath;

    public CharacterStats stats;


    [Space]
    public SpriteRigController rig;

    public string mainHandSocket;
    public string offHandSocket;

    public ItemBase mainHandItem;
    public ItemBase offHandItem;


    protected override void Awake() {
        if (stats == null) {
            stats = GetComponent<CharacterStats>();
        }
    }

    protected override void Start() {

    }

    protected override void Update() {

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

    //PLACE ITEM IN HAND
    public void PullOutItem(ItemBase item, Hand handToPutIn) {
        //if (item == null || !HasItem(item)) { return; }

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



    public virtual void Attack()
    { }

    public virtual void Defend()
    { }

    public virtual void EndDefend()
    { }

    public virtual void Move(float horizontal, float vertical)
    { }

    public virtual void Idle()
    { }

    public virtual void Interact()
    { }

    public virtual void Dodge() { }

    public virtual void SelectItemFromInventory(int hotbarNumber) { }

    public virtual void DamagePawn(float damageAmount) {

        stats.ApplyDamage(damageAmount);
        rig.StartFlashRig(2, .2f);

        if (stats.data[CharacterStats.StatType.Health] <= 0.0f) {
            KillPawn();
        }
    }

    public virtual void KillPawn() {

        //onPawnDeath();

        OnPawnDeath.Invoke();

        //Destroy(gameObject);
    }
}
