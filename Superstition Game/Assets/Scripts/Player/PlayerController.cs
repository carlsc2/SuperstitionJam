using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryController))]
public class PlayerController : MonoBehaviour {

    private InventoryController inventory;

    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject startingMainHandWeapon;
    public GameObject startingOffHandWeapon;

    [Space]
    public CharacterPawn possessedPawn;

    void Awake () 
    {
        possessedPawn = GetComponent<CharacterPawn>();
        if (possessedPawn == null) {
            GameObject playerIntance = (GameObject)GameObject.Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
            possessedPawn = playerIntance.GetComponent<CharacterPawn>();
        }


        inventory = GetComponent<InventoryController>();

        if (possessedPawn == null) { Debug.LogError("Controller does not possess pawn", this); }

        inventory.Init(possessedPawn);

        if (startingMainHandWeapon != null) {
            ItemBase createdItem = inventory.CreateAndAddToInventory(startingMainHandWeapon, possessedPawn);
            if (createdItem != null) {
                EquipItem(createdItem, CharacterPawn.Hand.Main);
            }
        }
        if (startingOffHandWeapon != null) {
            ItemBase createdItem = inventory.CreateAndAddToInventory(startingOffHandWeapon, possessedPawn);
            if (createdItem != null) {
                EquipItem(createdItem, CharacterPawn.Hand.Off);
            }
        }


    }
    
    void Update() {


        HandleInput();
    }

    private void HandleInput () 
    {
        if (possessedPawn == null) { return; }

        //if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
            possessedPawn.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //}
        //else
        //{
            //p.Idle();
        //}

        if(Input.GetButtonDown("Attack"))
        {
            possessedPawn.Attack();
        }

        if(Input.GetButtonDown("Defend"))
        {
            possessedPawn.Defend();
        }

        if (Input.GetButtonUp("Defend"))
        {
            possessedPawn.EndDefend();
        }

        if(Input.GetButtonDown("Interact"))
        {
            possessedPawn.Interact();
        }

        if (Input.GetButtonDown("Dodge")) {
            possessedPawn.Dodge();
        }

        if (Input.GetButtonDown("Hotbar1")) {
            possessedPawn.SelectItemFromInventory(0);
        }
        if (Input.GetButtonDown("Hotbar2")) {
            possessedPawn.SelectItemFromInventory(1);
        }
    }

    public void EquipItem(ItemBase item, CharacterPawn.Hand hand) {
        possessedPawn.PullOutItem(item, hand);
    }

    public void UnequipItem(ItemBase item, CharacterPawn.Hand hand) {
        possessedPawn.PutAwayCurrentItem(hand);
    }

}
