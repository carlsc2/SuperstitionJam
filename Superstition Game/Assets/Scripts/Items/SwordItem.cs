using UnityEngine;
using System.Collections;

public class SwordItem : ItemBase {

    public float damage;

    public bool canDamage = false;

    void OnTriggerEnter2D(Collider2D other) {

        //don't proceed unless we can damage
        if (!canDamage) { return; }

        //only deal damage if the other thing can deal damage
        if (other.gameObject.GetComponent<CharacterStats>() == null) { return; }

        //WE'RE GOOD TO GO

        CharacterStats otherStats = other.GetComponent<CharacterStats>();

        otherStats.DamageCharacter(damage);

    }


	
}
