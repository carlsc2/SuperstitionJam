﻿using UnityEngine;
using System.Collections;

public class SwordItem : ItemBase {

    public enum AttackType {
        Swing,
        Stab,
        AOE,
    }

    public AttackType weaponAttackType;

    public float damage;

    public bool canDamage = false;

    void OnTriggerEnter2D(Collider2D other) {

        //connection to owner is not set, do not proceed
        if (owner == null) { return; }

        //don't proceed unless we can damage
        if (!canDamage) { return; }

        //Damage Filter
        if (other.transform.root.GetComponent<CharacterStats>() == null         // Other doesn't have a CharacterStats
            || other.transform.root.GetComponent<CharacterPawn>() == null       // Other does not derive from Pawn
            || other.transform.root.gameObject == owner.gameObject) { return; } // Other is the Owner of this Item

        //WE'RE GOOD TO GO

        CharacterPawn otherPawn = other.transform.root.GetComponent<CharacterPawn>();

        float strengthMultiplier = 1.0f;
        CharacterStats ownerStats = owner.GetComponent<CharacterStats>();
        if (ownerStats != null) {
            strengthMultiplier = ownerStats.data[CharacterStats.StatType.Strength];
        }

        otherPawn.DamagePawn(damage * strengthMultiplier);

    }


    public override void BeginUseItem() {
        base.BeginUseItem();

        switch (weaponAttackType) {
            case AttackType.Swing:
                owner.GetComponent<AnimatorHandler>().Attack_Swing();
                break;

            case AttackType.Stab:
                owner.GetComponent<AnimatorHandler>().Attack_Stab();
                break;

            case AttackType.AOE:

                break;

            default: goto case AttackType.Swing;
        }

        canDamage = true;
    }

    public override void EndUseItem() {
        base.EndUseItem();

        canDamage = false;

        //owner
    }

}
