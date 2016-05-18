using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryController))]
public class PlayerController : CharacterPawnController {



    protected override void Awake () 
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);

    }
    
    protected override void Update() {
        base.Update();

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



}
