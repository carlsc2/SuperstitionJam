using UnityEngine;
using System.Collections;

public class PlayerAnimHandler : AnimatorHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack_Swing() {
        base.Attack_Swing();


    }

    public virtual void StartBlock() {
        anim.SetBool("Bock_Bool", true);
    }

    public virtual void EndBlock() {
        anim.SetBool("Block_Bool", false);
    }
}
