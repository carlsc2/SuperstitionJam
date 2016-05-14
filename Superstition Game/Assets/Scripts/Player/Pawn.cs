using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public CharacterStats stats;

    public SpriteRigController rig;


    protected virtual void Awake() {
        if (stats == null) {
            stats.GetComponent<SpriteRigController>();
        }
    }

    protected virtual void Start() {

    }

    protected virtual void Update() {

    }



    public virtual void Attack()
    { }

    public virtual void Defend()
    { }

    public virtual void EndDefend()
    { }

    public virtual void Move(float horizontal, float vertical)
    { }

    public virtual void Idle()
    { }

    public virtual void Interact()
    { }

    public virtual void DamagePawn(float damageAmount) {

        rig.StartFlashRig(2, .2f);
    }
}
