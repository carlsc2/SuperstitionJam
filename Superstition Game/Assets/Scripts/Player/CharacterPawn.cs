using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterPawn : Pawn {
    /*
    [System.Serializable]
    private class ItemInteractor {
        [HideInInspector]
        public Hand side;

        public ItemBase item;
        private Coroutine usingItemCoroutine;
        private IEnumerator UseItemTimer(float numSec, ItemBase item) {

            while (numSec > 0.0f) {
                numSec -= Time.deltaTime;

                yield return null;
            }

            //if item was destroyed by now, leave early
            if (item == null) { yield break; }

            item.EndUseItem();

            yield break;
        }
        public void SetItemTimer(float numSec) {
            //usingItemCoroutine 
            
        }
    }
    */

    protected MovementMotor motor;
    //InventoryController inventory;
    protected AnimatorHandler anim;

    protected HashSet<Transform> interactables;

    protected Vector3 oscale;



    public enum Hand {
        Main = 0,
        Off = 1,
    }

    //public delegate void OnDeathCallback();
    //OnDeathCallback onPawnDeath;

    //public PlayerController owningController;


    public enum PlayerPawnState { Idle, Walk, Attack, Defend };
    public enum Direction { Left, Right }


    public PlayerPawnState currentState = PlayerPawnState.Idle;
    protected Direction currentFace = Direction.Left;


    public float defendModifier = 3f;


    public UnityEvent OnPawnDeath;

    public CharacterStats stats;


    [Space]
    public SpriteRigController rig;

    public string mainHandSocket;
    public string offHandSocket;

    public ItemBase mainHandItem;
    public ItemBase offHandItem;

    private Coroutine usingItemCoroutine = null;
    //timer countdown funcitonality for EndUseItem()


//COLLISION
    void OnTriggerEnter2D(Collider2D col) {
        //update interaction list
        Interactable tmp = col.transform.root.GetComponentInChildren<Interactable>();
        if (tmp != null) {
            interactables.Add(col.transform.root);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        //update interaction list
        interactables.Remove(col.transform.root);
    }

//INITIALIZATION
    protected override void Awake() {
        if (stats == null) {
            stats = GetComponent<CharacterStats>();
        }

        motor = GetComponent<MovementMotor>();
        //inventory = GetComponent<InventoryController>();
        anim = GetComponent<AnimatorHandler>();
        //audSource = GetComponent<AudioSource>();

        interactables = new HashSet<Transform>();
        oscale = transform.localScale;
    }

    protected override void Start() {
        base.Start();
    }

//UPDATE
    protected override void Update() {
        base.Update();
        HandleAnimation();
    }

//ITEM USAGE

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

        Debug.Log("end Use Item in hand");

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


    public void EndUseItemByInstance(ItemBase item) {
        if (item == null) { return; }

        //Filter for item this Pawn owns
        if (item == mainHandItem 
         || item == offHandItem)
        {
            //item.EndUseItem();//End Item Usage Here
            currentState = PlayerPawnState.Idle;
            
        }

        //IMPLEMENT ENDING USAGE OF ANY INSTANCE OF AN ITEM
        else {
            Debug.LogWarning("Character Pawn does not own the Supplied Item; Implement Stop Any Item Here", this);
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


//STATE MACHINE ACTIONS
    public virtual void Attack()
    {
        if (currentState != PlayerPawnState.Attack && currentState != PlayerPawnState.Defend) {
            currentState = PlayerPawnState.Attack;
            //swordHitbox.SetActive(true);
            //stats.BeginUseItemInHand(CharacterStats.Hand.Main);
            BeginUseItemInHand(Hand.Main);
        }

        Debug.Log("Begin Attack");
    }

    void EndAttack() {
        currentState = PlayerPawnState.Idle;

        //stats.EndUseItemInHand(CharacterStats.Hand.Main);
        EndUseItemInHand(Hand.Main);


        Debug.Log("End Attack");
        //swordHitbox.SetActive(false);
    }

    public virtual void Defend(){
        if (currentState != PlayerPawnState.Attack && currentState != PlayerPawnState.Defend) {
            currentState = PlayerPawnState.Defend;
            //stats.BeginUseItemInHand(CharacterStats.Hand.Off);
            BeginUseItemInHand(Hand.Off);

            stats.data[CharacterStats.StatType.MaxSpeed] /= defendModifier;
        }
    }

    public virtual void EndDefend() {
        if (currentState == PlayerPawnState.Defend) {
            currentState = PlayerPawnState.Idle;
            //stats.EndUseItemInHand(CharacterStats.Hand.Off);
            EndUseItemInHand(Hand.Off);

            stats.data[CharacterStats.StatType.MaxSpeed] *= defendModifier;
        }
    }


    public virtual void Move(float horizontal, float vertical) {
        //if (currentState != PlayerPawnState.Attack) {
            /*
            if (currentState == PlayerPawnState.Defend) {
                motor.Move(horizontal, vertical);
            }
            */
           // else {
                motor.Move(horizontal, vertical);
                if (horizontal > 0) {
                    currentFace = Direction.Right;
                    transform.localScale = new Vector3(oscale.x, oscale.y, oscale.z);
                }
                else if (horizontal < 0) {
                    currentFace = Direction.Left;
                    transform.localScale = new Vector3(-oscale.x, oscale.y, oscale.z);
                }

                //ANIMATION

                if (currentState != PlayerPawnState.Walk)
                    currentState = PlayerPawnState.Walk;
            //}
        //}
    }

    /*
    public virtual void Idle() {
        if (currentState != PlayerPawnState.Attack && currentState != PlayerPawnState.Defend && currentState != PlayerPawnState.Idle) {
            currentState = PlayerPawnState.Idle;
        }
    }
    */

    public virtual void Interact() {
        //interact with nearest thing
        Transform nearest = null;
        float min_dist = Mathf.Infinity;
        Vector3 current_pos = transform.position;
        foreach (Transform t in interactables) {
            if (t == null) { continue; }

            float dist = Vector3.Distance(t.position, current_pos);
            if (dist < min_dist) {
                nearest = t;
                min_dist = dist;
            }
        }
        if (nearest != null) {
            nearest.GetComponentInChildren<Interactable>().Interact(transform);

            anim.Interact();
        }
    }

    private IEnumerator DodgeTimer(float time, float speed, float accel) {

        motor.isDodging = true;
        motor.accel = accel;
        motor.speed = speed;
        motor.trueMoveDirec = motor.desiredMoveDirec.normalized * speed;

        yield return new WaitForSeconds(time);


        motor.isDodging = false;
        motor.accel = stats.data[CharacterStats.StatType.Acceleration];
        motor.speed = stats.data[CharacterStats.StatType.MaxSpeed];

        yield return null;
    }

    public virtual void Dodge() {
        StartCoroutine(DodgeTimer(stats.data[CharacterStats.StatType.DodgeTime],
                          stats.data[CharacterStats.StatType.DodgeMaxSpeed],
                          stats.data[CharacterStats.StatType.DodgeAcceleration]));

    }

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


        //Trigger death animation on the model
        anim.TriggerDeath();



        //detatch the model from the GameObject that drives logic
        rig.transform.parent = null;

        //Destroy this gameobject
        Destroy(gameObject);


        OnPawnDeath.Invoke();

        //Destroy(gameObject);
    }

    protected void HandleAnimation() {
        anim.SetWalkBlend(motor.trueMoveDirec.magnitude);

    }
}
