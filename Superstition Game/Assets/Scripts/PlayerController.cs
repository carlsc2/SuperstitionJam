using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    MovementMotor mm; 


	void Start () 
    {
        mm = GetComponent<MovementMotor>();
	}
	
	void Update () 
    {
	    if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            mm.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
	}
}
