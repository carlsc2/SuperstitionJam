using UnityEngine;
using System.Collections;

public class AnimatorHandler : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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

}
