using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Pawn p;

    void Start () 
    {

        

        p = GetComponent<Pawn>();

    }
    
    void Update () 
    {
        //if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //{
            p.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //}
        //else
        //{
            //p.Idle();
        //}

        if(Input.GetButtonDown("Attack"))
        {
            p.Attack();
        }

        if(Input.GetButtonDown("Defend"))
        {
            p.Defend();
        }

        if (Input.GetButtonUp("Defend"))
        {
            p.EndDefend();
        }

        if(Input.GetButtonDown("Interact"))
        {
            p.Interact();
        }

        if (Input.GetButtonDown("Dodge")) {
            p.Dodge();
        }

        if (Input.GetButtonDown("Hotbar1")) {
            p.SelectItemFromInventory(0);
        }
        if (Input.GetButtonDown("Hotbar2")) {
            p.SelectItemFromInventory(1);
        }
    }
}
