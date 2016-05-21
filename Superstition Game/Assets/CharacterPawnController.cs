using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryController))]
public class CharacterPawnController : ControllerBase {

    protected InventoryController inventory;


    [Header("Prefabs")]
    public GameObject CharacterPawnPrefab;
    public GameObject startingMainHandWeapon;
    public GameObject startingOffHandWeapon;

    [Space]
    public CharacterPawn possessedPawn;


    protected override void Awake() {
        base.Awake();

        possessedPawn = GetComponent<CharacterPawn>();
        if (possessedPawn == null) {
            GameObject playerIntance = (GameObject)GameObject.Instantiate(CharacterPawnPrefab, transform.position, Quaternion.identity);
            possessedPawn = playerIntance.GetComponent<CharacterPawn>();
            possessedPawn.owningController = this;
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

    public virtual void EquipItem(ItemBase item, CharacterPawn.Hand hand) {
        possessedPawn.PullOutItem(item, hand);
    }

    public virtual void UnequipItem(ItemBase item, CharacterPawn.Hand hand) {
        possessedPawn.PutAwayCurrentItem(hand);
    }
}
