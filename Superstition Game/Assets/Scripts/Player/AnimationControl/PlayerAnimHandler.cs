using UnityEngine;
using System.Collections;

public class PlayerAnimHandler : AnimatorHandler {


    public virtual void StartBlock() {
        anim.SetBool("Bock_Bool", true);
    }

    public virtual void EndBlock() {
        anim.SetBool("Block_Bool", false);
    }
}
