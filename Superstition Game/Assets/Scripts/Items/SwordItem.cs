using UnityEngine;
using System.Collections;

public class SwordItem : ItemBase {

    public float damage;

    public bool canDamage = false;

    void OnTriggerEnter2D(Collider2D other) {

        //don't proceed unless we can damage
        if (!canDamage) { return; }

        //only deal damage if the other thing can deal damage
        if (other.transform.root.GetComponent<CharacterStats>() == null
            || other.transform.root.GetComponent<Pawn>() == null
            || other.transform.root.gameObject == owner.gameObject) { return; }

        //WE'RE GOOD TO GO

        Pawn otherPawn = other.transform.root.GetComponent<Pawn>();

        float strengthMultiplier = 1.0f;
        CharacterStats ownerStats = owner.GetComponent<CharacterStats>();
        if (ownerStats != null) {
            strengthMultiplier = ownerStats.strength;
        }

        otherPawn.DamagePawn(damage * strengthMultiplier);

    }


    public override void BeginUseItem() {
        base.BeginUseItem();

        owner.GetComponent<AnimatorHandler>().Attack_Swing();

        canDamage = true;
    }

    public override void EndUseItem() {
        base.EndUseItem();

        canDamage = false;
    }

}
