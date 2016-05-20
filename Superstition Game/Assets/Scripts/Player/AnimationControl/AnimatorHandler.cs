using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementMotor))]
public class AnimatorHandler : MonoBehaviour {

    public Animator anim;
    public MovementMotor motor;

    protected virtual void Awake() {

    }

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
    /*
    public void SetBool(string parameter, bool value) {
        anim.SetBool(parameter, value);
    }
    */

    public virtual void SetWalkBlend(float blendValue) {
        anim.SetFloat("WalkBlend_Float", blendValue);
    }

    public virtual void Attack_Swing() {
        anim.SetTrigger("Attack_Swing_Trig");

    }

    public virtual void Attack_Stab() {
        anim.SetTrigger("Attack_Stab_Trig");
    }

    public virtual void GetHit() {
        anim.SetTrigger("GetHit_Trig");
    }

    public virtual void TriggerDeath() {
        anim.SetTrigger("IsDead_Trig");
        GetHit();
    }

    public virtual void RaiseShield() {
        anim.SetBool("Block_Bool", true);
    }

    public virtual void LowerShield() {
        anim.SetBool("Block_Bool", false);
    }

    public virtual void Interact() {
        anim.SetTrigger("Use_Trig");
    }

}
